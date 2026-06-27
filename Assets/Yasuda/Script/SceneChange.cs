
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{

    [SerializeField]
    private Object sceneAsset; // Sceneを直接ドラッグ
    private string sceneName;

   

    private void Awake()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }

    private void Start()
    {
        //シーン開始時にIrisInを動かす
        IrisTransition.Instance.StartIrisIn();
    }


  

    public void SceneChangeManager() // インスペクターで指定
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("シーンが設定されていません");
        }

    }
}
