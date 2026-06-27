using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] private string bgmName = "TitleBGM"; // 再生するBGMの名前

    // Start is called before the first frame update
    void Start()
    {
        // ステージが始まったときにBGMを鳴らす
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBGM(bgmName);
        }
    }

    private void OnEnable()
    {
        // シーンがアンロード（破棄）されるときのイベントに、自分の関数を登録する
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // オブジェクトが破棄されるときは、登録を解除する（メモリリーク防止）
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // シーン遷移が始まって、現在のシーンが閉じられるときに自動で呼ばれる
    private void OnSceneUnloaded(Scene currentScene)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopBGM();
        }
    }
}
