using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.ResetScore();
        ScoreManager.Instance.SetScore(99);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
