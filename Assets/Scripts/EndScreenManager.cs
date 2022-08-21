using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public float winHappiness = 60;
    public GameObject winCondition;
    public GameObject loseCondition;
    public GameObject metCoworker;

    public TextMeshProUGUI happinessLevel;
    public TextMeshProUGUI comfortLevel;
    public TextMeshProUGUI moneyLevel;
    
    void Start()
    {
        if (GameManager.Instance.Happiness >= winHappiness)
        {
            winCondition.SetActive(true);
        }
        else
        {
            loseCondition.SetActive(true);
        }

        if (GameManager.Instance.HasMet)
        {
            metCoworker.SetActive(true);
        }

        happinessLevel.text = GameManager.Instance.Happiness + "";
        comfortLevel.text = GameManager.Instance.Comfort + "";
        moneyLevel.text = GameManager.Instance.Balance + "";
    }
}
