using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int _id;
    public List<KeyCode> _keysToPress = new List<KeyCode>();
    public Dictionary<KeyCode, bool> _keyStates = new Dictionary<KeyCode, bool>();
    public KeyCode _keyToRelease = KeyCode.None; // このターンに離すべきキー
}

public class TurnHandler : MonoBehaviour
{

    [SerializeField]
    private SceneChange sceneChange;

    Player[] _players = new Player[2];
    Player _currentPlayer = null;

    KeyStateManager _keyStateManager;
    [SerializeField]
    private KeyboardViewManager _keyboardViewManager;

    enum State
    {
        None = 0,
        GameStart,
        WaitForKeyPress,
        WaitForKeyRelease,
        WaitForPlayerChange,
    }
    State _currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        _players[0] = new Player { _id = 1 };
        _players[1] = new Player { _id = 2 };

        _currentPlayer = _players[0];
        
        _keyStateManager = new KeyStateManager();
        _keyStateManager.Initialize();

        // ゲームを開始状態にする
        _currentState = State.GameStart;
    }

    // Update is called once per frame
    void Update()
    {
        // 常に「維持すべきキー」が押し続けられているかをチェック（離したらゲームオーバー）
        CheckKeepPressing();

        switch (_currentState)
        {
            case State.None:
                break;
            
            case State.GameStart:
                SetupNextTurn();
                break;

            case State.WaitForKeyPress:
                // 指定のキーが押されたらプレイヤー交代へ
                CheckNewKeyPress();
                break;

            case State.WaitForKeyRelease:
                // 指定のキーが離されたらプレイヤー交代へ
                CheckKeyRelease();
                break;

            case State.WaitForPlayerChange:
                // プレイヤー交代ウェイト中
                break;
        }
    }

    // 次のターン（キー追加、またはキー解放）をセットアップする
    void SetupNextTurn()
    {
        Player opponent = (_currentPlayer == _players[0]) ? _players[1] : _players[0];

        // すでに3つのキーを押しているなら、次はキーを離す指示を出す
        if (_currentPlayer._keysToPress.Count >= KeyStateManager.MAX_KEYS_TO_PUSH)
        {
            _keyStateManager.SelectRandomReleaseKey(_currentPlayer);
            _currentState = State.WaitForKeyRelease;
        }
        else
        {
            // 3つ未満なら、新しくキーを押す指示を出す
            _keyStateManager.SelectRandomKey(_currentPlayer, opponent);
            _currentState = State.WaitForKeyPress;
        }
    }

    // 新たに指定されたキーが押されたかチェックする
    void CheckNewKeyPress()
    {
        if (_currentPlayer._keysToPress.Count == 0) return;

        // 最後にリストに追加された（最新の）キーがターゲット
        KeyCode targetKey = _currentPlayer._keysToPress[_currentPlayer._keysToPress.Count - 1];
        _keyboardViewManager.SetKeyboardView(targetKey);

        //押せ!!テキストを表示する
        _keyboardViewManager.ShowPressText(targetKey);

        // InputService からこのフレームで新規に押されたキーを取得
        int count;
        KeyCode[] pushedKeys = InputService.GetPushKeys(out count);

        for (int i = 0; i < count; i++)
        {
            if (pushedKeys[i] == targetKey)
            {
                // 正しく押されたので状態を true (押されている) に更新
                _keyStateManager.UpdateKey(_currentPlayer, targetKey, true);
                Debug.Log($"[プレイヤー {_currentPlayer._id}] が指示キー {targetKey} を押しました！");
                
                TransitionToPlayerChange();

                // 押せ!!テキストを非表示にする
                _keyboardViewManager.HidePressText(targetKey);
                return;
            }
        }
    }

    // 離す指示のキーが離されたかチェックする
    void CheckKeyRelease()
    {
        if (_currentPlayer._keyToRelease == KeyCode.None) return;

        KeyCode targetKey = _currentPlayer._keyToRelease;

        //離せ!!テキストを表示する
        _keyboardViewManager.ShowReleaseText(targetKey);

        // 指定キーが離されたか (Input.GetKey が false になったか)
        if (!Input.GetKey(targetKey))
        {
            Debug.Log($"[プレイヤー {_currentPlayer._id}] が指示通りキー {targetKey} を離しました！");

            //離せ!!テキストを非表示にする
            _keyboardViewManager.HideReleaseText(targetKey);

            // プレイヤーの押下リスト・状態から削除
            _currentPlayer._keysToPress.Remove(targetKey);
            _currentPlayer._keyStates.Remove(targetKey);
            _currentPlayer._keyToRelease = KeyCode.None; // リセット
            
            _keyboardViewManager.ClearKeyboardView(targetKey);
            // プレイヤーは交代せず、同じプレイヤーに続けて新しいキーを押すよう指示する
            TransitionToSamePlayerPress();

        }
    }

    // プレイヤー交代処理を予約する
    void TransitionToPlayerChange()
    {
        _currentState = State.WaitForPlayerChange;
        Invoke("SwapPlayer", 1.0f);
    }

    // 同じプレイヤーで連続してキーを押すフェーズへの移行を予約する
    void TransitionToSamePlayerPress()
    {
        _currentState = State.WaitForPlayerChange;
        Invoke("SetupSamePlayerPress", 1.0f);
    }

    // 同じプレイヤーのまま、新キーを抽選して押し待ちフェーズに移行する
    void SetupSamePlayerPress()
    {
        Player opponent = (_currentPlayer == _players[0]) ? _players[1] : _players[0];

        // 交代せず、同一プレイヤーに新しいキーを割り当てる
        _keyStateManager.SelectRandomKey(_currentPlayer, opponent);
        _currentState = State.WaitForKeyPress;
    }

    public void SwapPlayer()
    {
        // プレイヤーを切り替える
        if (_currentPlayer == _players[0])
        {
            _currentPlayer = _players[1];
        }
        else
        {
            _currentPlayer = _players[0];
        }

        // 次のターンの準備へ
        SetupNextTurn();
    }

    // 押している状態がキープされているか毎フレーム監視（離したらゲームオーバー）
    void CheckKeepPressing()
    {
        if (_currentState == State.None) return;

        foreach (var player in _players)
        {
            foreach (var key in player._keysToPress)
            {
                // 離すよう指示されているキー（_keyToRelease）は監視から除外
                if (player._keyToRelease == key) continue;

                // すでに押されている状態（true）なのに、実際の入力が離れていたら
                if (player._keyStates[key] && !Input.GetKey(key))
                {
                    Debug.LogError($"[プレイヤー {player._id}] がキー {key} を離しました！ ゲームオーバー！");
                    _currentState = State.None;
                    Invoke("GameOver", 1.0f);
                    return;
                }
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("ゲームオーバー！シーン遷移します。");
        sceneChange.SceneChangeManager();
    }
}
