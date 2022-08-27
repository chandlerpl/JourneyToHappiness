using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    public PlayerInput input;
    public TextMeshProUGUI purchaseTextHolder;
    public GameObject purchaseText;
    
    private Item _currentItem;
    private void Start()
    {
        purchaseText.SetActive(false);
        
        GameManager.Instance.MouseInput.UnlockMouse(false);
/*
        InputActionMap map = input.currentActionMap;
        //InputActionMap map = InputManager.Instance.Controls.FindActionMap("Player");
        InputAction action = map.FindAction("Interact", false);
        if (action != null)
        {
            action.performed += PurchaseCurrent;
        }
        
        action = map.FindAction("CameraControl", false);
        if (action != null)
        {
            action.performed += RotateCamera;
        }
        
        action = map.FindAction("Pause", false);
        if (action != null)
        {
            action.performed += PauseGame;
        }

        map = input.actions.FindActionMap("UI");
        action = map.FindAction("Pause", false);
        if (action != null)
        {
            action.performed += ResumeGame;
        }*/
    }

    public void OnPause()
    {
        GameManager.Instance.PauseGame(true);
    }

    public void OnResume()
    {
        GameManager.Instance.ResumeGame();
    }
    
    public void OnCameraControl(InputValue context)
    {
        float val = context.Get<float>();
        if (val < 0)
        {
            CameraController.Instance.MoveLeft();
        }
        else
        {
            CameraController.Instance.MoveRight();
        }
    }
    
    public void OnInteract()
    {
        if (_currentItem == null)
            return;
        if (!_currentItem.gameObject.activeSelf)
        {
            _currentItem = null;
            return;
        }
        
        purchaseText.SetActive(false);
        _currentItem.Purchase();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            purchaseText.SetActive(true);
            _currentItem = other.gameObject.GetComponent<Item>();
            purchaseTextHolder.text = _currentItem.purchaseCost;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            purchaseText.SetActive(false);
            _currentItem = null;
        }
    }
}
