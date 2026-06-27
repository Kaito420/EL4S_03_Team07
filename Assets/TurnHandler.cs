using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int _id;

}

public class TurnHandler : MonoBehaviour
{

    Player[] _players = new Player[2];
    Player _currentPlayer = null;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Method:CheckBindKeyPress();

        switch (_currentState)
        {
            case State.None:
                break;
            
            case State.WaitForKeyPress:
                // 指定のキーが押されたらWaitForPlayerChangeへ
                break;
            case State.WaitForKeyRelease:
                // 指定のキーが離されたらPress待機へ
                break;
            case State.WaitForPlayerChange:
                // プレイヤーを切り替える
                Invoke("SwapPlayer", 1.0f);
                break;
        }
    }

    public void SwapPlayer()
    {// プレイヤーを切り替える関数
        if (_currentPlayer == _players[0])
        {
            _currentPlayer = _players[1];
        }
        else
        {
            _currentPlayer = _players[0];
        }

        // 3つ押している状態であればリリース待機へ
        // if(KeyList.Length == 3) _currentState = State.WaitForKeyRelease;
        _currentState = State.WaitForKeyPress;
    }

}
