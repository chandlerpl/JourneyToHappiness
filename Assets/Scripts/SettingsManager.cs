using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance { get => _instance; }

    public static event Action<string, float> ChangedVolumeSetting;
    
    [SerializeField]
    private float masterVolume = 1f;
    [SerializeField]
    private float musicVolume = 0.13f;
    
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            
            PlayerPrefs.SetFloat("MasterVolume", value);
            AudioListener.volume = masterVolume;
        }
    }

    public float MusicVolume
    {
        get => musicVolume;
        set 
        {
            musicVolume = value;
            
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
            ChangedVolumeSetting?.Invoke("Music", musicVolume);
        }
    }
    
    private void Awake()
    {
        if (_instance != null)
        {
            enabled = false;
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        
        AudioListener.volume = masterVolume;
    }
}
