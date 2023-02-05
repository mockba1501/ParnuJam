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

    public void AddItem(WordItem newItem)
    {
        this.wordItem = newItem;

        wordLabel = wordItem.word;

        if (newItem.type == 0)
        {
            icon = uiMngr.seedBagImg;
        }
        else if (newItem.type == 1 || newItem.type == 2)
        {
            icon = uiMngr.fertilizerBagImg;
        }
    }
}
