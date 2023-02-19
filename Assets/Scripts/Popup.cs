using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public TMP_Text messageText;

    public void AdjustPopupMessage(string message)
    {
        messageText.text = message;
    }

    public void DisplayPopUp()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePopUp()
    {
        this.gameObject.SetActive(false);
    }
}

