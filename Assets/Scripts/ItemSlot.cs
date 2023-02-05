using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using TMPro;

public class ItemSlot : MonoBehaviour
{

    public Image icon; // Previsously Image class
    public Button removeButton;
    public string wordLabel; // Previously Text class
    public int type; //0 root, 1 prefix, 2 suffix
    public WordItem wordItem;

    public UIManager uiMngr;
    public GameObject objectToFind;
    public GameObject gObjectToFind;
    public Sprite newSprite;
    public TMP_Text buttonText;

    private void Start()
    {
        objectToFind = transform.GetChild(0).gameObject;
        Debug.Log(objectToFind.name);

        // gObjectToFind = transform.GetChild(0).GetChild(0).gameObject;
        //Debug.Log(gObjectToFind.GetComponent<TMP_Text>().text);

        //newSprite = objectToFind.GetComponent<Image>().sprite;
        //Debug.Log(newSprite.name);

        //icon = objectToFind.GetComponent<Image>();

        //buttonText = gObjectToFind.GetComponent<TMP_Text>().text;

    }

    //Adding a new word to the item slot
    public void AddItem(WordItem newItem)
    {
        this.wordItem = newItem;
        this.type = newItem.type;

        wordLabel = wordItem.word;
        buttonText.text = wordItem.word;
        Debug.Log(newItem.type.GetType());

        if (this.type == 0)
        {
            icon = uiMngr.seedBagImg;
        }
        else
        {
            icon = uiMngr.fertilizerBagImg;
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
