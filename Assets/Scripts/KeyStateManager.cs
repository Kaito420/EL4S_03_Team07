using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全プレイヤーのキー入力状態の管理 / キーの抽選を行うクラス
/// </summary>
public class KeyStateManager
{
    public const int MAX_KEYS_TO_PUSH = 3;

    // 使用する範囲のキー(半角英文字のみ)のリスト
    public List<KeyCode> alphabetKeyCodes = new List<KeyCode>();

    public void Initialize()
    {
        // アルファベットの範囲のキーコードを定義
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            alphabetKeyCodes.Add((KeyCode)i);
        }
    }

    /// <summary>
    /// キーの押下状態を更新するメソッド
    /// </summary>
    /// <param name="player">対象のプレイヤー</param>
    /// <param name="keycode">対象キーの KeyCode</param>
    /// <param name="state">更新後の押下状態</param>
    public void UpdateKey(Player player, KeyCode keycode, bool state)
    {
        if (player._keyStates.ContainsKey(keycode))
        {
            player._keyStates[keycode] = state;
        }
    }

    /// <summary>
    /// 次の押すべきキーを抽選するメソッド
    /// </summary>
    /// <param name="targetPlayer">キーを更新するプレイヤー</param>
    /// <param name="otherPlayer">もう片方のプレイヤー</param>
    public void SelectRandomKey(Player targetPlayer, Player otherPlayer)
    {
        // 1P / 2P ともに使用されていないキーのリスト
        List<KeyCode> availableKeys = new List<KeyCode>();
        foreach (KeyCode key in alphabetKeyCodes)
        {
            // 1P / 2P どちらからも使用されていなければ追加
            if (!targetPlayer._keysToPress.Contains(key) && !otherPlayer._keysToPress.Contains(key))
            {
                availableKeys.Add(key);
            }
        }

        if (availableKeys.Count == 0)
        {
            Debug.LogWarning("使用可能なキーがありません。");
            return;
        }

        // 有効なキーの一覧から抽選
        int index = Random.Range(0, availableKeys.Count);
        KeyCode chosenKey = availableKeys[index];
        targetPlayer._keysToPress.Add(chosenKey);
        targetPlayer._keyStates[chosenKey] = false; // 最初は押されていない状態
    
        Debug.Log($"[プレイヤー {targetPlayer._id}]： 抽選された次押すべき KeyCode → {chosenKey}");
    }

    /// <summary>
    /// 次離すべきキーを抽選するメソッド
    /// </summary>
    /// <param name="player">キーを更新するプレイヤー</param>
    public void SelectRandomReleaseKey(Player player)
    {
        if (player._keysToPress.Count == 0) return;
        
        int index = Random.Range(0, player._keysToPress.Count);
        KeyCode key = player._keysToPress[index];
        
        player._keyToRelease = key;
        Debug.Log($"[プレイヤー {player._id}]： 抽選された次離すべき KeyCode → {key}");
    }
}
