using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IrisTransition : MonoBehaviour
{
    private IEnumerator ChangeScene()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1.2f);//‘Ò‚¿ŽžŠÔ‚ðì‚é

        sc.SceneChangeManager();
    }



    public static IrisTransition  Instance { get; private set; }

    [SerializeField]
    private Material m_irisMaterial;

    [SerializeField]
    private float m_duration = 1f;

    private float m_timer;

    private bool m_isPlaying;

    [SerializeField]
    private SceneChange sc;

    private enum IrisMode
    {
        None,
        IrisIn,
        IrisOut
    }

    private IrisMode m_mode;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Update()
    {
        if (!m_isPlaying)
            return;

        m_timer += Time.deltaTime;

        float t = Mathf.Clamp01(
            m_timer / m_duration);

        if (m_mode == IrisMode.IrisIn)
        {
            float radius =
                Mathf.Lerp(0f, 1f, t);

            m_irisMaterial.SetFloat(
                "_Radius",
                radius);

            if (t >= 1f)
            {
                m_isPlaying = false;
            }
        }

        if (m_mode == IrisMode.IrisOut)
        {
            float radius =
                Mathf.Lerp(1f, 0f, t);

            m_irisMaterial.SetFloat(
                "_Radius",
                radius);

            if (t >= 1.0f)
            {
                m_isPlaying = false;

                m_irisMaterial.SetFloat("_Radius", 0f);
                StartCoroutine(ChangeScene());
            }
        }
    }

    public void StartIrisIn()
    {
        m_timer = 0f;
        m_mode = IrisMode.IrisIn;
        m_isPlaying = true;

        m_irisMaterial.SetFloat(
            "_Radius",
            0f);
       
    }

    public void StartIrisOut()
    {
        m_timer = 0f;
        m_mode = IrisMode.IrisOut;
        m_isPlaying = true;

        m_irisMaterial.SetFloat(
            "_Radius",
            1f);
    }
}
