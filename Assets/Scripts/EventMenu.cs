using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventMenu : MonoBehaviour
{
    public delegate void EventCallback(bool result);

    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;

    public TextMeshProUGUI confirmButtonText;
    public TextMeshProUGUI declineButtonText;
    
    public Button declineButton;
    public Button confirmButton;

    private EventCallback callback;

    public void ShowMenu(string titleText, string descText, bool isContextMenu, EventCallback callback) {
        ShowMenu(titleText, descText, isContextMenu, isContextMenu ? "Confirm" : "Yes", "No", callback);
    }

    public void ShowMenu(string titleText, string descText, bool isContextMenu, string confirmText, string declineText, EventCallback callback) {
        title.text = titleText;
        desc.text = descText;

        declineButton.gameObject.SetActive(!isContextMenu);
        gameObject.SetActive(true);
        confirmButtonText.text = confirmText;
        declineButtonText.text = declineText;

        this.callback = callback;
    }

    public void HideMenu() {
        gameObject.SetActive(false);
    }

    public void Decline() {
        HideMenu();
        callback.Invoke(false);
    }

    public void Confirm() {
        HideMenu();
        callback.Invoke(true);
    }
}
