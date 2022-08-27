using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ScreenButtonSelector : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelectedButton;
    [SerializeField] private bool storePreviouslySelected;
    [SerializeField] private bool disablePreviouslySelected;
    private GameObject previouslySelectedButton;
    
    private void Start()
    {
        InputUser.onChange += OnControlsChanged;
        
        if (!GameManager.Instance.CheckControlScheme())
            return;
        
        if (storePreviouslySelected)
        {
            previouslySelectedButton = EventSystem.current.currentSelectedGameObject;
            if (disablePreviouslySelected)
            {
                previouslySelectedButton.transform.parent.gameObject.SetActive(false);
            }
        }
        
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
    }

    public void OnControlsChanged(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            if (!user.controlScheme.HasValue) return;
            string deviceName = user.controlScheme.Value.name;
            if (!GameManager.Instance.CheckControlScheme(deviceName))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
            }
        }
    }

    private void OnDisable()
    {
        InputUser.onChange -= OnControlsChanged;

        if (GameManager.Instance == null || !GameManager.Instance.CheckControlScheme())
            return;
        
        if (storePreviouslySelected && EventSystem.current != null)
        {
            if (disablePreviouslySelected)
            {
                previouslySelectedButton.transform.parent.gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        }
    }
    private void OnDestroy()
    {
        InputUser.onChange -= OnControlsChanged;
        
        if (GameManager.Instance == null || !GameManager.Instance.CheckControlScheme())
            return;
        
        if (storePreviouslySelected && EventSystem.current != null)
        {
            if (disablePreviouslySelected)
            {
                previouslySelectedButton.transform.parent.gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        }
    }
}
