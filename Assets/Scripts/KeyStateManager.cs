using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// 全プレイヤーのキー入力状態の管理 / キーの抽選を行うクラス
/// </summary>
public class KeyStateManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの Enum
    /// </summary>
    public enum Player
    {
        PLAYER_1,
        PLAYER_2,
    }

    public const int MAX_KEYS_TO_PUSH = 3;

    // 使用する範囲のキー(半角英文字のみ)のリスト
    public List<int> alphabetKeyCodes = new List<int>();

    // プレイヤー毎のキー状態の辞書
    private Dictionary<Player, Dictionary<int, bool>> keyStates = new Dictionary<Player, Dictionary<int, bool>>();
    
    // プレイヤー毎の押すべきキーリストの辞書
    private Dictionary<Player, List<int>> keysToPush = new Dictionary<Player, List<int>>();

    void Start()
    {
        // アルファベットの範囲のキーコードを定義
        for (int i = (int)KeyCode.A; i < (int)KeyCode.Z; i++)
        {
            alphabetKeyCodes.Add(i);
        }

        // プレイヤー毎のキーの状態
        keyStates[Player.PLAYER_1] = new Dictionary<int, bool>();
        keyStates[Player.PLAYER_2] = new Dictionary<int, bool>();

        // 押すべきキーの管理リストを初期化
        keysToPush[Player.PLAYER_1] = new List<int>();
        keysToPush[Player.PLAYER_2] = new List<int>();

        // 主要なKeyCodeの範囲で初期化
        foreach (int key in alphabetKeyCodes)
        {
            keyStates[Player.PLAYER_1][key] = false;
            keyStates[Player.PLAYER_2][key] = false;
        }
    }

    /// <summary>
    /// キーの押下状態を更新するメソッド
    /// </summary>
    /// <param name="player">現在のターンのプレイヤー</param>
    /// <param name="keycode">押されたキーの KeyCode</param>
    /// <param name="state">更新後の押下状態</param>
    public void UpdateKey(Player player, int keycode, bool state)
    {
        keyStates[player][keycode] = state;
    }


    /// <summary>
    /// 次の押すべきキーを抽選するメソッド
    /// </summary>
    /// <param name="player">キーを更新するプレイヤー<param>
    public void SelectRandomKey(Player player)
    {
        // 1P / 2P ともに使用されていないキーのリスト
        List<int> availableKeys = new List<int>();
        for (int i = (int)KeyCode.A; i < (int)KeyCode.Z; i++)
        {
            // 1P / 2P どちらからも使用されていなければ追加
            if (!keysToPush[Player.PLAYER_1].Contains(i) && !keysToPush[Player.PLAYER_2].Contains(i))
            {
                availableKeys.Add(i);
            }
        }

        // 有効なキーの一覧から抽選
        int index = Random.Range(0, availableKeys.Count);
        int key = availableKeys[index];
        keysToPush[player].Add(key);
    
        UnityEngine.Debug.Log($"{((player == Player.PLAYER_1) ? "[プレイヤー1]" : "[プレイヤー2]")}： 抽選された次押すべき KeyCode → {(char)key}");
    }

    
    /// <summary>
    /// 次離すべきキーを抽選するメソッド
    /// </summary>
    /// <param name="player">キーを更新するプレイヤー</param>
    public void  SelectRandomReleaseKey(Player player)
    {
        int index = Random.Range(0, MAX_KEYS_TO_PUSH);
        UnityEngine.Debug.Log($"{((player == Player.PLAYER_1) ? "[プレイヤー1]" : "[プレイヤー2]")}： 抽選された次離すべき KeyCode → {(char)keysToPush[player][index]}");
        keysToPush[player].RemoveAt(index);
    }
}
