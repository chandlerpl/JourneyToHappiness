using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("The amount of money an item costs. Negative values will increase the players Balance.")]
    public float cost;
    public float happinessChange;
    public float comfortChange;

    public List<GameObject> showObjects;
    public List<GameObject> hideObjects;

    public bool advanceCurrentDay;
    
    public void Purchase()
    {
        GameManager.Instance.Comfort += comfortChange;
        GameManager.Instance.Happiness += happinessChange;
        GameManager.Instance.Balance -= cost;

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
}
