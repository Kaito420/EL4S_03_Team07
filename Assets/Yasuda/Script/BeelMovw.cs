using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeelMovw : MonoBehaviour
{
    [SerializeField]
    private float beelTime = 3.0f;
    [SerializeField]
    SceneChange sc;


    private float nowTime = 0;

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.time;
        if(nowTime >= beelTime)
        {
            sc.SceneChangeManager();
        }
    }
}
