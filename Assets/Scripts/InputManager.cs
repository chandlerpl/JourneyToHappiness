using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    //public static InputManager Instance { get => _instance; }
    
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private string startingMap;
    public InputActionAsset Controls
    {
        get => actions;
    }
    
    private void Start()
    {
        if (_instance != null)
        {
            enabled = false;
            return;
        }
        _instance = this;
        
        SwitchMap(startingMap);
    }

    public void SwitchMap(string name)
    {
        foreach (InputActionMap map in Controls.actionMaps)
        {
            if (map.name.Equals(name))
            {
                map.Enable();
            }
            else
            {
                map.Disable();
            }
        }
    }
}
