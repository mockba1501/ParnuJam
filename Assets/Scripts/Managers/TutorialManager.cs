using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<Popup> popupMessages;
    private int popupIndex;

    // Start is called before the first frame update
    void Start()
    {
        popupIndex = 0;
        DisplayTip();
    }

    public void DisplayTip()
    {
        popupMessages[popupIndex].DisplayPopUp();
    }
    public void MoveToNextTip()
    {
        popupMessages[popupIndex].HidePopUp();
        popupIndex++;
        popupMessages[popupIndex].DisplayPopUp();
    }

    public void ExitPopUp()
    {
        popupMessages[popupIndex].HidePopUp();
    }
}
