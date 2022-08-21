using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    private void Start()
    {
        GameManager.ComfortChanged += comfort => { Debug.Log("" + comfort); };
        GameManager.HappinessChanged += happiness => { Debug.Log("" + happiness); };
        GameManager.BalanceChanged += balance => { Debug.Log("" + balance); };
        GameManager.CurrentDayChanged += newDay => { Debug.Log("" + newDay); };
    }

}
