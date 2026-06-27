using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardViewManager : MonoBehaviour
{
    [Header("左プレイヤーのキー表示リスト")]
    [SerializeField] private List<Text> m_leftPlayerKeyList = new List<Text>();

    [Header("右プレイヤーのキー表示リスト")]
    [SerializeField] private List<Text> m_rightPlayerKeyList = new List<Text>();

    [Header("バツマークのプレハブ")]
    [SerializeField] private Image m_crossImage;

    [Header("左の押せ!!テキストオブジェクトリスト")]
    [SerializeField] private List<GameObject> m_leftPressTextObjects = new List<GameObject>();

    [Header("左の離せ!!テキストオブジェクトリスト")]
    [SerializeField] private List<GameObject> m_leftReleaseTextObjects = new List<GameObject>();

    [Header("右の押せ!!テキストオブジェクトリスト")]
    [SerializeField] private List<GameObject> m_rightPressTextObjects = new List<GameObject>();

    [Header("右の離せ!!テキストオブジェクトリスト")]
    [SerializeField] private List<GameObject> m_rightReleaseTextObjects = new List<GameObject>();

    public enum PlayerType  // プレイヤーの種類を表す列挙型
    {
        Left,
        Right
    }

    private PlayerType m_currentPlayerType = PlayerType.Left;   // 現在のプレイヤーの種類を保持する変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // キーボードの指示UIをセットするメソッド
    public void SetKeyboardView(KeyCode key)
    {
        //表示の最大数を取得
        int count = Mathf.Min(m_leftPlayerKeyList.Count,m_rightPlayerKeyList.Count);

        // 空いている場所を探す
        for (int i = 0; i < count; i++)
        {
            if(m_leftPlayerKeyList[i].text == key.ToString() || m_rightPlayerKeyList[i].text == key.ToString())
            {
                // すでに表示されている場合は何もしない
                return;
            }
            // 左側が空いている
            if (string.IsNullOrEmpty(m_leftPlayerKeyList[i].text))
            {
                m_leftPlayerKeyList[i].text = key.ToString();
                m_currentPlayerType = PlayerType.Left;
                return;
            }

            // 右側が空いている
            if (string.IsNullOrEmpty(m_rightPlayerKeyList[i].text))
            {
                m_rightPlayerKeyList[i].text = key.ToString();
                m_currentPlayerType = PlayerType.Right;
                return;
            }
        }

        // 空いている場所がない場合はランダムに上書きする
        int randomIndex = Random.Range(0, count);

        if (m_currentPlayerType == PlayerType.Left)
        {
            m_rightPlayerKeyList[randomIndex].text = key.ToString();
            m_currentPlayerType = PlayerType.Right;
        }
        else
        {
            m_leftPlayerKeyList[randomIndex].text = key.ToString();
            m_currentPlayerType = PlayerType.Left;
        }
    }

    //引数のキーコードに対応するテキストを消すメソッド
    public void ClearKeyboardView(KeyCode key)
    {
        // 左プレイヤーのキー表示リストを検索して、対応するテキストを消す
        foreach (Text text in m_leftPlayerKeyList)
        {
            if (text.text == key.ToString())
            {
                text.text = string.Empty;
                return;
            }
        }
        // 右プレイヤーのキー表示リストを検索して、対応するテキストを消す
        foreach (Text text in m_rightPlayerKeyList)
        {
            if (text.text == key.ToString())
            {
                text.text = string.Empty;
                return;
            }
        }
    }

    //指定したキーコードに対応するテキストの「押せ」テキスト表示
    public void ShowPressText(KeyCode key)
    {
        // 左プレイヤーのキー表示リストを検索して、対応する「押せ」テキストを表示
        for (int i = 0; i < m_leftPlayerKeyList.Count; i++)
        {
            if (m_leftPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を黒色に変更
                m_leftPlayerKeyList[i].color = Color.black;

                m_leftPressTextObjects[i].SetActive(true);
                return;
            }
        }
        // 右プレイヤーのキー表示リストを検索して、対応する「押せ」テキストを表示
        for (int i = 0; i < m_rightPlayerKeyList.Count; i++)
        {
            if (m_rightPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を黒色に変更
                m_rightPlayerKeyList[i].color = Color.black;

                m_rightPressTextObjects[i].SetActive(true);
                return;
            }
        }
    }

    //指定したキーコードに対応するテキストの「押せ」テキスト非表示
    public void HidePressText(KeyCode key)
    {
        // 左プレイヤーのキー表示リストを検索して、対応する「押せ」テキストを非表示
        for (int i = 0; i < m_leftPlayerKeyList.Count; i++)
        {
            if (m_leftPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を白色に変更
                m_leftPlayerKeyList[i].color = Color.white;

                m_leftPressTextObjects[i].SetActive(false);
                return;
            }
        }
        // 右プレイヤーのキー表示リストを検索して、対応する「押せ」テキストを非表示
        for (int i = 0; i < m_rightPlayerKeyList.Count; i++)
        {
            if (m_rightPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を白色に変更
                m_rightPlayerKeyList[i].color = Color.white;

                m_rightPressTextObjects[i].SetActive(false);
                return;
            }
        }
    }

    //指定したキーコードに対応するテキストの「離せ」テキスト表示
    public void ShowReleaseText(KeyCode key)
    {
        // 左プレイヤーのキー表示リストを検索して、対応する「離せ」テキストを表示
        for (int i = 0; i < m_leftPlayerKeyList.Count; i++)
        {
            if (m_leftPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を赤色に変更
                m_leftPlayerKeyList[i].color = Color.red;
                m_leftReleaseTextObjects[i].SetActive(true);
                return;
            }
        }
        // 右プレイヤーのキー表示リストを検索して、対応する「離せ」テキストを表示
        for (int i = 0; i < m_rightPlayerKeyList.Count; i++)
        {
            if (m_rightPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を赤色に変更
                m_rightPlayerKeyList[i].color = Color.red;
                m_rightReleaseTextObjects[i].SetActive(true);
                return;
            }
        }
    }

    //指定したキーコードに対応するテキストの「離せ」テキスト非表示
    public void HideReleaseText(KeyCode key)
    {
        // 左プレイヤーのキー表示リストを検索して、対応する「離せ」テキストを非表示
        for (int i = 0; i < m_leftPlayerKeyList.Count; i++)
        {
            if (m_leftPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を白色に変更
                m_leftPlayerKeyList[i].color = Color.white;

                m_leftReleaseTextObjects[i].SetActive(false);
                return;
            }
        }
        // 右プレイヤーのキー表示リストを検索して、対応する「離せ」テキストを非表示
        for (int i = 0; i < m_rightPlayerKeyList.Count; i++)
        {
            if (m_rightPlayerKeyList[i].text == key.ToString())
            {
                //キーテキストの色を白色に変更
                m_rightPlayerKeyList[i].color = Color.white;

                m_rightReleaseTextObjects[i].SetActive(false);
                return;
            }
        }
    }



    //指定した場所にバツマークを表示するメソッド
    public void ShowCrossMark(int index, PlayerType playerType)
    {
        if (playerType == PlayerType.Left && index >= 0 && index < m_leftPlayerKeyList.Count)
        {
            // 左プレイヤーの指定した場所にバツマークを表示
            GameObject cross = Instantiate(m_crossImage.gameObject, m_leftPlayerKeyList[index].transform);
            cross.transform.localPosition = Vector3.zero; // バツマークをテキストの中央に配置
        }
        else if (playerType == PlayerType.Right && index >= 0 && index < m_rightPlayerKeyList.Count)
        {
            // 右プレイヤーの指定した場所にバツマークを表示
            GameObject cross = Instantiate(m_crossImage.gameObject, m_rightPlayerKeyList[index].transform);
            cross.transform.localPosition = Vector3.zero; // バツマークをテキストの中央に配置
        }
    }
}
