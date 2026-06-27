using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    private void Awake()
    {
        // すでに存在する場合は重複生成を防ぐ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
 
    /// スコアを加算
    /// <param name="value">加算する値</param>
    public void AddScore(int value)
    {
        Score += value;
    }

    /// スコアをリセット
    public void ResetScore()
    {
        Score = 0;
    }

    /// スコアを直接設定
    public void SetScore(int value)
    {
        Score = value;
    }
}
