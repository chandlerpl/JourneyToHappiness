using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    private GameObject currentItem;
    [SerializeField] InputActionAsset m_Actions;
    private void Start()
    {
        InputAction action = m_Actions.FindAction("Interact", false);
        if (action != null)
        {
            action.performed += PurchaseCurrent;
        }
    }

    private void PurchaseCurrent(InputAction.CallbackContext context)
    {
        if (currentItem != null)
        {
            currentItem.GetComponent<Item>().Purchase();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = null;
        }
    }
}
