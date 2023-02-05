using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    public GameManager gameManager;
    public Transform itemsOnSale;
    public Sprite seedBagImg; // previously Image class
    public Sprite fertilizerBagImg; // previously Image class
    ItemSlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsOnSale.GetComponentsInChildren<ItemSlot>();
        InitilizeWordSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void InitilizeWordSlots()
    {
        int i = 0;
        foreach (var slot in slots) 
        {
            i++;
            WordItem tmp = gameManager.GetWord();
            slot.AddItem(tmp);
            Debug.Log($"Word added {i} {tmp.word} of type {tmp.type}");
        }
    }

    //Read the next top words and display them to be shown next
    public void GetNextWords()
    {
        //Specify the number of words to be displayed

        //Call the manager and retrieve a list of next words

        List<string> topWord = gameManager.GetNextWords(3);
        foreach (string word in topWord) 
        {
            Debug.Log("NEXT WORD TO DISPLAY " + word);
        }
    }
}
