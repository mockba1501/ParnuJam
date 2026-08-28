using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    //public WordManager wordManager;
    //public GameManager gameManager;

    public Sprite seedBagImg; // previously Image class
    public Sprite fertilizerBagImg; // previously Image class
    
    public ItemSlot[] itemSlots;
    public GameObject[] futureSlots;

    public Popup gameOverPopup;
    public Popup confirmationPopup;

    List<TMP_Text> futureWords = new List<TMP_Text>();
    [SerializeField]
    private TMP_Text instructionSlot;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text wordsCounterText;
    [SerializeField]
    private TMP_Text wordsTargetText;

    private int futureWordsDisplayMax;

    private void OnEnable()
    {
        PlantManager.OnPlantSold += HandlePlantSold;
        PlantManager.OnPlantInstructionRequested += UpdateInstructionMessage;
        ItemSlot.OnItemSlotInstructionRequested += UpdateInstructionMessage;

        GameManager.OnMoneyChanged += HandleMoneyChanged;
    }
    private void OnDisable()
    {
        PlantManager.OnPlantSold -= HandlePlantSold;
        PlantManager.OnPlantInstructionRequested -= UpdateInstructionMessage;
        ItemSlot.OnItemSlotInstructionRequested -= UpdateInstructionMessage;
        GameManager.OnMoneyChanged -= HandleMoneyChanged;
    }

    void Awake()
    {
        //Debug.Log("Accessing UI Manager Awake " + System.DateTime.Now.Month.ToString());
        if (Instance != null && Instance != this)
        {
            //Debug.LogWarning("More than one instance of Inventory found");
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
        UpdatedWordsGeneratedCounter();
        UpdatedWordsTargetText();

        //UpdateCoinsDisplay();
        // If GameManager started first, OnMoneyChanged already set it.
        // As a safety fallback for Start():
        if (GameManager.Instance != null)
            HandleMoneyChanged(GameManager.Instance.CurrentMoney());
    }

    private void HandlePlantSold(int value)
    {
        //UpdateCoinsDisplay();
        UpdateInstructionMessage("Congratulations you generated some money!");
        Invoke("ShowPlantSeedPrompt", 1f);
    }

    
    private void ShowPlantSeedPrompt()
    {
        UpdateInstructionMessage("Choose a seed to plant!");
    }
    
    public void InitilizeWordSlots()
    {
        foreach (var slot in itemSlots) 
        {
            if (!WordManager.Instance.IsEmpty())
            {
                WordItem tmp = WordManager.Instance.GetWord();
                slot.AddItem(tmp);
            }
            //If the word queue is empty then you need to clear the slot?
          //  Debug.Log($"Word added {i} {tmp.word} of type {tmp.type}");
        }
    }

    public void ActivateSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.EnableItemSlotButtons();
        }
    }

    public void DisableSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.DisableItemSlotButtons();
        }
    }

    //Read the next top words and display them to be shown next
    public void GetNextWords()
    {
        //Call the manager and retrieve a list of next words
        List<string> topWord = WordManager.Instance.GetNextWords(futureWordsDisplayMax);

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
        if (!WordManager.Instance.IsEmpty())
        {
            WordItem tmp = WordManager.Instance.GetWord();
            slot.AddItem(tmp);
            return true;
        }
        return false;
    }

    public void UpdateInstructionMessage(string txt) 
    {
        instructionSlot.text = txt;
    }
/*
    public void UpdateCoinsDisplay()
    {
        coinText.text = gameManager.CurrentMoney().ToString();
    }
*/
    private void HandleMoneyChanged(int currentMoney)
    {
        coinText.text = currentMoney.ToString();
    }

    public void UpdatedWordsGeneratedCounter() 
    {
        wordsCounterText.text = GameManager.Instance.CurrentWordsGeneratedCounter().ToString();
    }

    public void UpdatedWordsTargetText()
    {
        wordsTargetText.text = "Out of " + GameManager.Instance.GetWordWinningTarget().ToString();
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
    public void OpenPopUp(Popup popupPanel) 
    {
        popupPanel.DisplayPopUp();
    }

    public void ClosePopUp(Popup popupPanel)
    {
        popupPanel.HidePopUp();
    }

    public void UpdateGameOverMessage(string txt)
    {
        gameOverPopup.AdjustPopupMessage(txt);
        OpenPopUp(gameOverPopup);
    }
}
