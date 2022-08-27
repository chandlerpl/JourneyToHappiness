using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ScreenButtonSelector : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelectedButton;
    [SerializeField] private bool storePreviouslySelected;
    [SerializeField] private bool disablePreviouslySelected;
    private GameObject previouslySelectedButton;
    
    private void Start()
    {
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

    private void OnDisable()
    {
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
