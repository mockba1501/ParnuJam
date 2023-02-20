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
        //DisplayTip();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popupMessages.Count; i++)
        {
            if(i == popupIndex)
            {
                popupMessages[i].gameObject.SetActive(true);
            }
        }
    }
    */

    // Open or close popups
    public void OpenPopUp(GameObject popUp)
    {
        popUp.SetActive(true);
    }

    public void ClosePopUp(GameObject popUp)
    {
        popUp.SetActive(false);
    }
    void DisplayTip()
    {
        popupMessages[popupIndex].gameObject.SetActive(true);
    }
    void MoveToNextTip()
    {
        popupMessages[popupIndex].gameObject.SetActive(false);
        popupIndex++;
        popupMessages[popupIndex].gameObject.SetActive(true);
    }
}
