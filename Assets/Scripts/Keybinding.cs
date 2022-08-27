using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keybinding : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button keyButton;
    public TextMeshProUGUI keyText;

    public void Set(string name, string key)
    {
        nameText.text = name;
        keyText.text = key;
    }
}
