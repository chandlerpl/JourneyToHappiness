using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Disabled Options")]
    public List<GameObject> onlyEnabledInCertainDevices;
    
    [Header("Audio Settings")]
    public GameObject audioPanel;
    public Slider masterVolume;
    public Slider musicVolume;
    
    [Header("Graphics Settings")]
    public GameObject graphicsPanel;
    
    [Header("Controls Settings")]
    public GameObject controlsPanel;
    public InputActionAsset controls;
    public GameObject keybindingObject;
    
    [Header("Display Settings")]
    public GameObject displayPanel;
    public TMP_Dropdown displayOptions;
    public TMP_Dropdown resolutionOptions;

    private void Start()
    {
#if !UNITY_STANDALONE
        foreach (GameObject obj in onlyEnabledInCertainDevices)
        {
            obj.SetActive(false);
        }
#endif
    }

    public void OnAudioSelected()
    {
        controlsPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        displayPanel.SetActive(false);

        masterVolume.value = SettingsManager.Instance.MasterVolume;
        musicVolume.value = SettingsManager.Instance.MusicVolume;
        
        audioPanel.SetActive(true);
    }

    public void OnMasterVolumeChange()
    {
        SettingsManager.Instance.MasterVolume = masterVolume.value;
    }

    public void OnMusicVolumeChange()
    {
        SettingsManager.Instance.MusicVolume = musicVolume.value;
    }

    public void OnDisplaySelected()
    {
        controlsPanel.SetActive(false);
        audioPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        
        displayOptions.options.Clear();
        int displays = Display.displays.Length;
        for (int i = 0; i < displays; ++i)
        {
            displayOptions.options.Add(new TMP_Dropdown.OptionData("Monitor " + i));
        }
        displayOptions.onValueChanged.AddListener(i =>
        {
            StartCoroutine(TargetDisplay(i));
        });
        resolutionOptions.options.Clear();
        foreach(Resolution res in Screen.resolutions)
        {
            resolutionOptions.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionOptions.onValueChanged.AddListener(i =>
        {
            Resolution res = Screen.resolutions[i];
            Screen.SetResolution(res.height, res.width, Screen.fullScreenMode);
        });
        
        displayPanel.SetActive(true);
    }

    private IEnumerator TargetDisplay(int targetDisplay)
    {
        // Get the current screen resolution.
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
 
        // Set the target display and a low resolution.
        PlayerPrefs.SetInt("UnitySelectMonitor", targetDisplay);
        Screen.SetResolution(800, 600, Screen.fullScreen);

        // Wait a frame.
        yield return null;
 
        // Restore resolution.
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);
    }
    
    public void OnGraphicsSelected()
    {
        controlsPanel.SetActive(false);
        audioPanel.SetActive(false);
        displayPanel.SetActive(false);
        graphicsPanel.SetActive(true);
    }

    public void OnControlsSelected()
    {
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        displayPanel.SetActive(false);

        InputAction action = controls.FindActionMap("Player").FindAction("Move");
        
        action.PerformInteractiveRebinding(0);
        GameObject obj = Instantiate(keybindingObject, controlsPanel.transform);
        Keybinding bindings = obj.GetComponent<Keybinding>();
        bindings.Set("Walk Forward", action.bindings[0].ToDisplayString());
        bindings.keyButton.onClick.AddListener(() =>
        {
            bindings.keyText.text = "...";
            action.PerformInteractiveRebinding(0);
        });


        controlsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
