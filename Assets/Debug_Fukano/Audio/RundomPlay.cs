using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlay : MonoBehaviour
{
    [Header("この5つのリストからランダムで1つ鳴らします")]
    [SerializeField] private List<string> seNames = new List<string>();

    // Startはゲームが始まった瞬間に1回だけ実行される関数です
    void Start()
    {
        PlayRandomSEOnce();
    }

    /// <summary>
    /// リストの中からランダムに1つだけ選んでSEを再生する
    /// </summary>
    public void PlayRandomSEOnce()
    {
        // エラー防止：リストが空っぽなら処理を中断する
        if (seNames == null || seNames.Count == 0)
        {
            Debug.LogWarning("SEリストが空っぽです！Unityのインスペクターから名前を追加してください。");
            return;
        }

        // エラー防止：AudioManagerが存在しないなら中断する
        if (AudioManager.instance == null)
        {
            Debug.LogWarning("AudioManagerが見つかりません！シーンに配置されているか確認してください。");
            return;
        }

        // 0 から 「リストの要素数（5つなら5）」 の間でランダムな数字（0, 1, 2, 3, 4）を1つ選ぶ
        int randomIndex = Random.Range(0, seNames.Count);

        // 選ばれた番号のSE名を取り出す
        string selectedSEName = seNames[randomIndex];

        // 最初につくった通常のPlaySEを呼び出して、1回だけ鳴らす
        AudioManager.instance.PlaySE(selectedSEName);

        // ログにどれが流れたか表示（確認用）
        Debug.Log($"ランダム再生: {selectedSEName} を再生しました（要素番号: {randomIndex}）");
    }
}