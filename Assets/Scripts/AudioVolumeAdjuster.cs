using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeAdjuster : MonoBehaviour
{
    public string volumeName;
    
    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();

        if(SettingsManager.Instance != null)
            _source.volume = SettingsManager.Instance.MusicVolume;

        SettingsManager.ChangedVolumeSetting += ChangeVolume;
    }

    private void ChangeVolume(string volName, float volume)
    {
        if (volName.Equals(volumeName))
        {
            _source.volume = volume;
        }
    }

    private void OnDestroy()
    {
        SettingsManager.ChangedVolumeSetting -= ChangeVolume;
    }
}
