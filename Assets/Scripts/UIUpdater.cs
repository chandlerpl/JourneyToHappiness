using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI comfortText;
    public TextMeshProUGUI day;
    private void Start()
    {
        GameManager.ComfortChanged += comfort => { comfortText.text = comfort.ToString(); };
        GameManager.HappinessChanged += happiness => { happinessText.text = happiness.ToString(); };
        GameManager.BalanceChanged += balance => { moneyText.text = balance.ToString(); };
        GameManager.CurrentDayChanged += newDay => { day.text = newDay.ToString(); };
        
        
    }

    
}
