using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{

    public Sprite icon; // Previsously Image class
    public Button removeButton;
    public string wordLabel; // Previously Text class
    public int type; //0 root, 1 prefix, 2 suffix
    public WordItem wordItem;

    public UIManager uiMngr;
    public GameObject objectToFind;
    public Sprite buttonSprite;

    private void Start()
    {
        objectToFind = transform.GetChild(0).gameObject;
        Debug.Log(objectToFind.name);

        buttonSprite = objectToFind.GetComponent<Image>().sprite;
        Debug.Log(buttonSprite.name);
        
    }

    //Adding a new word to the item slot
    public void AddItem(WordItem newItem)
    {
        this.wordItem = newItem;

        wordLabel = wordItem.word;

        if (newItem.type == 0)
        {
            buttonSprite = uiMngr.seedBagImg;
        }
        else if (newItem.type == 1 || newItem.type == 2)
        {
            buttonSprite = uiMngr.fertilizerBagImg;
        }
    }

    public void UseItem()
    {
        /*
        //When you click on an item either two things happen:
        //1) If it was a root word and there are empty spaces in the field plant it

        //2) If it was a fertilizer:
            a) you select a correct root combination it will grow to the following level
            b) if incorrect root nothing will happen


        */
    }

    /*
    public void ClearSlot()
    {
        //this.wordItem = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    */
}
