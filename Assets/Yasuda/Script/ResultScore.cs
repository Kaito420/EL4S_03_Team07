using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    public int resultScore;
    Text resultScoreText;
    // Start is called before the first frame update
    void Start()
    {
        resultScoreText = GetComponent<Text>();
        resultScore = ScoreManager.Instance.Score;
        resultScoreText.text = string.Format("{0:D2}", resultScore);
    }

   
}
