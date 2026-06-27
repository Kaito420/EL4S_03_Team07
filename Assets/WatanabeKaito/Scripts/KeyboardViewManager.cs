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
