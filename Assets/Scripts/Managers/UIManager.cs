using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public WordManager wordManager;
    public GameManager gameManager;

    public static UIManager uiManager { get; private set; }

    public Sprite seedBagImg; // previously Image class
    public Sprite fertilizerBagImg; // previously Image class
    
    public ItemSlot[] itemSlots;
    public GameObject[] futureSlots;

    public GameObject[] popups;

    List<TMP_Text> futureWords = new List<TMP_Text>();
    [SerializeField]
    private TMP_Text instructionSlot;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text wordsGeneratedCounterText;

    private int futureWordsDisplayMax;

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

        //Specify the number of words to be displayed
        futureWordsDisplayMax = futureSlots.Count();

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
        //Call the manager and retrieve a list of next words
        List<string> topWord = wordManager.GetNextWords(futureWordsDisplayMax);

        int i = 0;
        foreach (string word in topWord) 
        {
            //Debug.Log("NEXT WORD TO DISPLAY " + word);
            futureWords[i].text= word;
            i++;
        }

        //Condition to empty slots in case number of generated words is less than available slots
        if(topWord.Count() < futureWordsDisplayMax)
        {
            for(; i< futureWordsDisplayMax; i++) 
            {
                futureWords[i].text = "";
            }
        }
    }

    public bool IsRootAvailable()
    {
        foreach (var slot in itemSlots)
        {
            if (slot.wordItem.type == 0 && slot.IsSlotActive())
            {
                return true;
            }
        }
        return false;
    }

    public bool IsSlotsEmpty()
    {
        foreach (var slot in itemSlots)
        {
            if (slot.IsSlotActive())
            {
                return false;
            }
        }
        return true;
    }

    public bool RefreshSlot(ItemSlot slot)
    {
        if (!wordManager.IsEmpty())
        {
            WordItem tmp = wordManager.GetWord();
            slot.AddItem(tmp);
            return true;
        }
        return false;
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

    // Sound mute toggler
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    // Open or close popups
    public void OpenPopUp(GameObject popUp) 
    {
        popUp.SetActive(true);
    }

    public void ClosePopUp(GameObject popUp)
    {
        popUp.SetActive(false);
    }
}
