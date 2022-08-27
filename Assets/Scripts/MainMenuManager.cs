using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        GameManager.Reset();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        GameManager.Reset();
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }
}
