using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MonoBehaviour
{
     private Text textObject;

    [Header("点滅間隔")]
    [SerializeField] private float m_blinkInterval = 0.5f;

    [Header("時間差表示の有無")]
    [SerializeField] private bool m_isTimeLagDisplay = false;

    [Header("時間差表示の時間")]
    [SerializeField] private float m_timeLagDuration = 2.0f;

    private float m_timeLag = 0.0f;

    private bool m_isBlinking = false;


    private void Start()
    {
        textObject = GetComponent<Text>();

        if (m_isTimeLagDisplay)
        {
            // 最初は透明
            Color color = textObject.color;
            color.a = 0f;
            textObject.color = color;
        }
        else
        {
            m_isBlinking = true;
        }
    }

    private void Update()
    {
        // 一定時間待つ
        if (!m_isBlinking && m_isTimeLagDisplay)
        {
            m_timeLag += Time.deltaTime;

            if (m_timeLag >= m_timeLagDuration)
            {
                m_isBlinking = true;
            }
            else
            {
                return;
            }
        }

        if(m_isBlinking)
        {
            // 点滅処理
            float alpha =
                Mathf.PingPong(Time.time, m_blinkInterval) / m_blinkInterval;

            Color color = textObject.color;
            color.a = alpha;
            textObject.color = color;
        }

       
    }
}
