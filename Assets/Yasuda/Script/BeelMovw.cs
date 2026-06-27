using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeelMovw : MonoBehaviour
{
    [SerializeField]
    private float beelTime = 6.0f;
    


    private float nowTime = 0;

    // Update is called once per frame
    void Update()
    {
        nowTime += 1.0f / 60.0f;
        if(nowTime >= beelTime)
        {
            IrisTransition.Instance.StartIrisOut();
        }
    }
}
