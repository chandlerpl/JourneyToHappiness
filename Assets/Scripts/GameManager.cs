using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[Serializable]
public class EventSettings {
    public string eventName;
    //public GameObject eventUI;
    public int eventStartDate;
    public int eventEndDate;
    
    public float comfortChange;
    public float happinessChange;
    public float cost;

    public bool isRandom;
    public string title;
    public bool isContext;
    public string confirmText  = "Yes";
    public string declineText= "No";

    [HideInInspector]
    public bool hasHadEvent;
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

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
    
    [Header("Event Section")]
    [SerializeField]
    private float dailyChance = 0.1f;
    [SerializeField]
    private float costToGoOut = 25f;
    [SerializeField]
    private float happinessIncrease = 5f;
    [SerializeField] 
    private EventMenu contextMenu;
    private bool _hasMet = false;
    public bool HasMet { get => _hasMet; }

    public List<EventSettings> eventSettings;

    [SerializeField]
    private ScoringSystem _happiness;
    [SerializeField]
    private ScoringSystem _comfort;
    [SerializeField]
    private ScoringSystem _balance;
    [SerializeField]
    private ScoringSystem _bills;
    [SerializeField]
    private ScoringSystem _day;

    public ScoringSystem Happiness { get => _happiness; }
    public ScoringSystem Comfort { get => _comfort; }
    public ScoringSystem Balance { get => _balance; }
    public ScoringSystem Bills { get => _bills; }
    public ScoringSystem Day { get => _day; }
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
    }

    public void AdvanceDay()
    {
        Day.Value += 1;
        _balance.Value -= _bills;
                
        if (Day > maxDays)
        {
            SceneManager.LoadScene(2);
        }

        foreach(EventSettings ev in eventSettings) {
            if(!ev.hasHadEvent && Day >= ev.eventStartDate && Day < ev.eventEndDate) {
                if(ev.isRandom) {
                    float val = Random.Range(0f, 1f);
                    if (val > dailyChance)
                    {
                        continue;
                    }
                }
                
                ev.hasHadEvent = true;
                contextMenu.ShowMenu(ev.title, "", ev.isContext, ev.confirmText, ev.declineText, b => {
                    if(b) {
                        Happiness.Value += ev.happinessChange;
                        Comfort.Value += ev.comfortChange;
                        Balance.Value -= ev.cost;
                        ResumeGame();
                    }
                });
                
                //ev.eventUI.SetActive(true);
                PauseGame(false);
            }
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
