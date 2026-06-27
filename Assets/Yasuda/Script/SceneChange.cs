
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    [SerializeField]
    private SceneAsset sceneName;


    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName.name);
    }
}
