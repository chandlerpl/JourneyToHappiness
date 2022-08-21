using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{

    public TextMeshProUGUI purchaseTextHolder;
    public GameObject purchaseText;
    
    private Item _currentItem;
    [SerializeField] InputActionAsset m_Actions;
    private void Start()
    {
        purchaseText.SetActive(false);
        
        InputAction action = m_Actions.FindAction("Interact", false);
        if (action != null)
        {
            action.performed += PurchaseCurrent;
        }
        
        action = m_Actions.FindAction("CameraControl", false);
        if (action != null)
        {
            action.performed += RotateCamera;
        }
    }

    private void RotateCamera(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<float>();
        if (val < 0)
        {
            CameraController.Instance.MoveLeft();
        }
        else
        {
            CameraController.Instance.MoveRight();
        }
    }
    
    private void PurchaseCurrent(InputAction.CallbackContext context)
    {
        if (_currentItem != null)
        {
            purchaseText.SetActive(false);
            _currentItem.Purchase();
        }
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
