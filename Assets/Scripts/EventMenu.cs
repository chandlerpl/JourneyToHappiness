using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventMenu : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;

    public TextMeshProUGUI confirmButtonText;
    public TextMeshProUGUI declineButtonText;
    
    public Button declineButton;
    public Button confirmButton;

    public void ShowMenu(string titleText, string descText, bool isContextMenu) {
        ShowMenu(titleText, descText, isContextMenu, isContextMenu ? "Confirm" : "Yes", "No");
    }

    public void ShowMenu(string titleText, string descText, bool isContextMenu, string confirmText, string declineText) {
        title.text = titleText;
        desc.text = descText;

        declineButton.gameObject.SetActive(!isContextMenu);

        confirmButtonText.text = confirmText;
        declineButtonText.text = declineText;
    }


}
