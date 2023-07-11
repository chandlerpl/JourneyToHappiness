using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public TextMeshProUGUI CharacterName;

    public GameObject PlayerObjectCooper;
    public GameObject PlayerObjectAmy;
    // Start is called before the first frame update
   

    public void ChangeNameCooper()
    {
        CharacterName.SetText("Cooper");
        PlayerObjectCooper.SetActive(true);
        PlayerObjectAmy.SetActive(false);
    }

    public void ChangeNameAmy()
    {
        CharacterName.SetText("Amy");
        PlayerObjectCooper.SetActive(false);
        PlayerObjectAmy.SetActive(true);
    }

    // Update is called once per frame

    
}
