using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            int a = IrisTransition.Instance.GetMode();
            //Debug.Log(a);

            if (a==2)
            {
                IrisTransition.Instance.StartIrisOut();
                //Debug.Log("アイリス開始");
            }
           
           
        }

    }
}
