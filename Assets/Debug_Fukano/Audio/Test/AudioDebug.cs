using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    [SerializeField] private KeyCode testKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        // UIボタンが押されたときなどにSEを鳴らす
        AudioManager.instance.PlaySE("ClickSE");

        // ステージが始まったときにBGMを鳴らす
        AudioManager.instance.PlayBGM("Stage1BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(testKey))
        {
            // スペースキーが押されたときにSEを鳴らす
            AudioManager.instance.PlaySE("JumpSE");
        }
    }
}
