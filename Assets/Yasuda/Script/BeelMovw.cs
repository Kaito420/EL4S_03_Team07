using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeelMovw : MonoBehaviour
{
    [SerializeField]
    private float beelTime = 6.0f;
    


    private float nowTime = 0;
    bool a = false;
    // Update is called once per frame
    void Update()
    {

        nowTime += Time.deltaTime;
        //Debug.Log(nowTime);
        if(nowTime >= beelTime && a==false)
        {
            a = true;
            IrisTransition.Instance.StartIrisOut();
        }
    }
}
