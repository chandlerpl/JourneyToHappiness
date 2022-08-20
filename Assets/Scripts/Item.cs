using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float cost;
    public float happinessChange;
    public float comfortChange;

    private void Start()
    {
        StartCoroutine(PurchaseTest());
    }

    public void Purchase()
    {
        GameManager.Instance.Comfort += comfortChange;
        GameManager.Instance.Happiness += happinessChange;
        GameManager.Instance.Balance -= cost;
    }
    
    IEnumerator PurchaseTest()
    {
        yield return new WaitForSeconds(10f);
        Purchase();
    }
}
