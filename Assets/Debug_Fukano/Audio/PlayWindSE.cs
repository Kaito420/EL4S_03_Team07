using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWindSE : MonoBehaviour
{
    [Header("再生するSEの名前")]
    [SerializeField] private string seName = "Wind_03"; // 再生するSEの名前

    [Header("SEを鳴らす間隔")]
    [SerializeField] private float seInterval = 20.0f; // SEを鳴らす間隔（秒）
    private float dt = 0.0f; // 経過時間を記録する変数

    // Start is called before the first frame update
    void Start()
    {
        // ステージが始まったときにSEを鳴らす
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySE(seName);
        }

    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime; // 経過時間を更新

        if (dt > seInterval)
        {
            AudioManager.instance.PlaySE(seName); // SEを鳴らす
        }
    }
}
