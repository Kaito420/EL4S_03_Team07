using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTitleSE : MonoBehaviour
{
    [Header("再生するSEの名前")]
    [SerializeField] private string seName = "start"; // 再生するSEの名前

    private bool hasPlayed = false; // SEが再生されたかどうかを追跡するフラグ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPlayed)
        {
            return; // すでにSEが再生されている場合は何もしない
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.instance.PlaySE(seName);
            hasPlayed = true; // SEが再生されたことを記録
        }
    }
}
