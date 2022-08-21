using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    
    public static event Action<float> HappinessChanged;
    public static event Action<float> ComfortChanged;
    public static event Action<float> BalanceChanged;
    public static event Action<int> CurrentDayChanged;
    
    [SerializeField]
    private float balance;
    [SerializeField]
    private float startingHappiness;
    [SerializeField]
    private float startingComfort;
    [SerializeField]
    private float maxHappiness;
    [SerializeField]
    private float maxComfort;
    
    private float _happiness;
    private float _comfort;

    private int _currentDay = 1;
    
    public float Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            if (_happiness > maxHappiness)
                _happiness = maxHappiness;
            HappinessChanged?.Invoke(_happiness);
        }
    }
    
    public float Comfort
    {
        get => _comfort;
        set
        {
            _comfort = value;
            if (_comfort > maxComfort)
                _comfort = maxComfort;
            ComfortChanged?.Invoke(_comfort);
        }
    }
    
    public float Balance
    {
        get => balance;
        set
        {
            balance = value;
            BalanceChanged?.Invoke(balance);
        }
    }
    
    public float MaxHappiness { get => maxHappiness; }
    
    public float MaxComfort { get => maxComfort; }
    
    public int CurrentDay { 
        get => _currentDay;
        private set
        {
            _currentDay = value;
            CurrentDayChanged?.Invoke(_currentDay);
        }
    }
    
    private void Start()
    {
        if (_instance != null)
        {
            this.enabled = false;
            return;
        }

        _instance = this;
        StartCoroutine(SetStartingValues());
    }

    private IEnumerator SetStartingValues()
    {
        yield return new WaitForFixedUpdate();
        Happiness = startingHappiness;
        Comfort = startingComfort;
    }

    public void AdvanceDay()
    {
        CurrentDay += 1;
    }
}
