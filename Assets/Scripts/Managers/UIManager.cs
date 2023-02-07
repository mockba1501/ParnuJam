using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    public WordManager wordManager;

    public Sprite seedBagImg; // previously Image class
    public Sprite fertilizerBagImg; // previously Image class
    
    public ItemSlot[] itemSlots;
    public GameObject[] futureSlots;
    List<TMP_Text> futureWords = new List<TMP_Text>();

    void Awake()
    {
        if (uiManager != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        uiManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in futureSlots) 
        {
            futureWords.Add(item.GetComponentInChildren<TMP_Text>());
        }
        InitilizeWordSlots();
        GetNextWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void InitilizeWordSlots()
    {
        foreach (var slot in itemSlots) 
        {
            WordItem tmp = wordManager.GetWord();
            slot.AddItem(tmp);
          //  Debug.Log($"Word added {i} {tmp.word} of type {tmp.type}");
        }
    }

    //Read the next top words and display them to be shown next
    public void GetNextWords()
    {
        //Specify the number of words to be displayed

        //Call the manager and retrieve a list of next words

        List<string> topWord = wordManager.GetNextWords(3);
        int i = 0;
        foreach (string word in topWord) 
        {
            //Debug.Log("NEXT WORD TO DISPLAY " + word);
            futureWords[i].text= word;
            i++;
        }

    }
}
