using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IrisTransition.Instance.StartIrisOut();
            Debug.Log("A");
        }

    }
}
