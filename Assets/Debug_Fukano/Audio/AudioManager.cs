using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // AudioMixerを使うために必要

public class AudioManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SESource;

    [Header("Audio Mixer Group (音量管理用)")]
    [SerializeField] private AudioMixerGroup BGMGroup;
    [SerializeField] private AudioMixerGroup SEGroup;

    [Header("Audio Clips")]
    [SerializeField] private List<SoundData> BGMList;
    [SerializeField] private List<SoundData> SEList;

    // 検索を高速化するための辞書
    private Dictionary<string, AudioClip> bgmDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> seDictionary = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public struct SoundData
    {
        public string name;
        public AudioClip clip;
    }

    void Awake()
    {
        // シングルトンの確定とシーン間保持
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- データ初期化 ---
    private void InitializeData()
    {
        // リストから辞書へ変換して検索しやすくする
        foreach (var data in BGMList) bgmDictionary[data.name] = data.clip;
        foreach (var data in SEList) seDictionary[data.name] = data.clip;

        // AudioSourceにミキサーグループを割り当て
        if (BGMGroup != null) BGMSource.outputAudioMixerGroup = BGMGroup;
        if (SEGroup != null) SESource.outputAudioMixerGroup = SEGroup;

        BGMSource.loop = true; // BGMは基本ループ
    }

    // --- BGM再生機能 ---
    public void PlayBGM(string name)
    {
        if (bgmDictionary.TryGetValue(name, out AudioClip clip))
        {
            if (BGMSource.clip == clip) return; // 既に同じ曲が流れていたら何もしない
            BGMSource.clip = clip;
            BGMSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM: {name} が見つかりません");
        }
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }

    // --- SE再生機能 ---
    public void PlaySE(string name)
    {
        if (seDictionary.TryGetValue(name, out AudioClip clip))
        {
            // PlayOneShotは複数のSEが重なって再生できる
            SESource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SE: {name} が見つかりません");
        }
    }



}