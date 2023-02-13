using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public WordManager wordManager;
    public GameManager gameManager;

    public static UIManager uiManager { get; private set; }


    public Sprite seedBagImg; // previously Image class
    public Sprite fertilizerBagImg; // previously Image class
    
    public ItemSlot[] itemSlots;
    public GameObject[] futureSlots;
    List<TMP_Text> futureWords = new List<TMP_Text>();
    [SerializeField]
    private TMP_Text instructionSlot;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text wordsGeneratedCounterText;

    void Awake()
    {
        //Debug.Log("Accessing UI Manager Awake " + System.DateTime.Now.Month.ToString());
        if (uiManager != null)
        {
            //Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        uiManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Accessing UI Manager Start " + System.DateTime.Now.Month.ToString());

        foreach (var item in futureSlots) 
        {
            futureWords.Add(item.GetComponentInChildren<TMP_Text>());
        }
        InitilizeWordSlots();
        GetNextWords();
        UpdateCoinsDisplay();
        UpdatedWordsGeneratedCounter();
    }

    //
    public void InitilizeWordSlots()
    {
        foreach (var slot in itemSlots) 
        {
            if (!wordManager.IsEmpty())
            {
                WordItem tmp = wordManager.GetWord();
                slot.AddItem(tmp);
            }
            //If the word queue is empty then you need to clear the slot?
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

    public void RefreshSlot(ItemSlot slot)
    {
        if (!wordManager.IsEmpty())
        {
            WordItem tmp = wordManager.GetWord();
            slot.AddItem(tmp);
        }
    }

    public void UpdateInstructionMessage(string txt) 
    {
        instructionSlot.text = txt;
    }

    public void UpdateCoinsDisplay()
    {
        coinText.text = gameManager.CurrentMoney().ToString();
    }

    public void UpdatedWordsGeneratedCounter() 
    {
        wordsGeneratedCounterText.text = gameManager.CurrentWordsGeneratedCounter().ToString();
    }
}
