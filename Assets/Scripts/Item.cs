using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public AudioSource purchasedItemSound;
    public AudioSource noMoneySound;
    public string purchaseCost;
    
    [Tooltip("The amount of money an item costs. Negative values will increase the players Balance.")]
    public float cost;
    public float happinessChange;
    public float comfortChange;
    public float billsChange;

    public List<GameObject> showObjects;
    public List<GameObject> hideObjects;

    public bool advanceCurrentDay;
    
    public void Purchase()
    {
        if (GameManager.Instance.Balance - cost > 0)
        {
           
            GameManager.Instance.Comfort += comfortChange;
            GameManager.Instance.Happiness += happinessChange;
            GameManager.Instance.Balance -= cost;
            GameManager.Instance.Bills += billsChange;
            if (purchasedItemSound != null)
            {
                purchasedItemSound.Play();
            }
            foreach (GameObject obj in showObjects)
            {
                
                obj.SetActive(true);
            }
            foreach (GameObject obj in hideObjects)
            {
                obj.SetActive(false);
            }

            if (advanceCurrentDay)
            {
                GameManager.Instance.AdvanceDay();
            }
        }
        else
        {
            if (noMoneySound != null)
            {
                noMoneySound.Play();
            }
            // Add something here if they don't have enough money? 
        }
    }
}
