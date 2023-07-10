using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public static event Action<float> HappinessChanged;
    public static event Action<float> ComfortChanged;
    public static event Action<float> BalanceChanged;
    public static event Action<float> BillsChanged;
    public static event Action<int> CurrentDayChanged;

    [Header("Player Input")]
    [SerializeField]
    private PlayerInput input;
    [SerializeField] 
    private StarterAssetsInputs mouseInput;
    [SerializeField] private List<string> autoSelectButtonSchemes;
    
    public PlayerInput PlayerInput { get => input; }
    
    public StarterAssetsInputs MouseInput { get => mouseInput; }
    
    [Header("Pause Menu")]
    [SerializeField] 
    private GameObject pauseMenu;
    [SerializeField] 
    private GameObject gameUI;

    [Header("Gameplay Settings")]
    [SerializeField]
    private float maxDays = 30;
    
    [SerializeField]
    private float balance;
    [SerializeField]
    private float bills;
    [SerializeField]
    private float startingHappiness;
    [SerializeField]
    private float startingComfort;
    [SerializeField]
    private float maxHappiness;
    [SerializeField]
    private float maxComfort;
    
    [Header("Romance Section")]
    [SerializeField]
    private float minimumStartDay = 5;
    [SerializeField]
    private float dailyChance = 0.1f;
    [SerializeField]
    private float costToGoOut = 25f;
    [SerializeField]
    private float happinessIncrease = 5f;
    [SerializeField] 
    private GameObject meetCoworker;
    private bool _hadChanceToMeet = false;
    private bool _hasMet = false;
    
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

    public float Bills
    {
        get => bills;
        set
        {
            bills = value;
            BillsChanged?.Invoke(bills);
        }
    }
    
    public bool HasMet
    {
        get => _hasMet;
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
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
            //this.enabled = false;
            //return;
        }

        DontDestroyOnLoad(gameObject);
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
        Balance -= Bills;

        if (!_hadChanceToMeet && CurrentDay > minimumStartDay)
        {
            float val = Random.Range(0f, 1f);
            if (val < dailyChance)
            {
                _hadChanceToMeet = true;
                meetCoworker.SetActive(true);
                PauseGame(false);
            }
        }
        
        if (CurrentDay > maxDays)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void PauseGame(bool showMenu)
    {
        Time.timeScale = 0f;
        mouseInput.UnlockMouse(false);
        input.SwitchCurrentActionMap("UI");

        //InputManager.Instance.SwitchMap("UI");
        
        if (showMenu)
        {
            pauseMenu.SetActive(true);
            gameUI.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        mouseInput.UnlockMouse(true);
        input.SwitchCurrentActionMap("Player");
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }
    
    public void MeetCoworker(bool yes)
    {
        if (yes)
        {
            _hasMet = true;
        }
        meetCoworker.SetActive(false);
        ResumeGame();
        Happiness += happinessIncrease;
        Balance -= costToGoOut;
    }

    public bool CheckControlScheme([CanBeNull] string deviceName = null)
    {
        deviceName ??= PlayerInput.currentControlScheme;
        return autoSelectButtonSchemes.Contains(deviceName);
    }
    
    public static void Reset()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            _instance = null;
        }
    }
}
