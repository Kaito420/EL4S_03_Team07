using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MonoBehaviour
{
     private Text textObject;

    [Header("ì_ñ≈ä‘äu")]
    [SerializeField] private float m_blinkInterval = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // ì_ñ≈èàóù
        if (textObject != null)
        {
            float alpha = Mathf.PingPong(Time.time, m_blinkInterval) / m_blinkInterval;
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, alpha);
        }
    }
}
