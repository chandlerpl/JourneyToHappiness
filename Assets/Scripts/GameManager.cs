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
    
    private float _happiness;
    private float _comfort;
    [SerializeField]
    private float balance;

    public float Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            HappinessChanged?.Invoke(_happiness);
        }
    }
    
    public float Comfort
    {
        get => _comfort;
        set
        {
            _comfort = value;
            ComfortChanged?.Invoke(_comfort);
        }
    }
    
    public float Balance
    {
        get => balance;
        set
        {
            balance = value;
            HappinessChanged?.Invoke(balance);
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
    }
}
