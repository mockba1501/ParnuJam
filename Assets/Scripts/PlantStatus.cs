using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantStatus : MonoBehaviour
{
    public string rootWord;
    public bool isEmpty;
    public int level;
    public int wordValue;
    public string currentWord;

    //public GameObject carrotPrefab;
    [SerializeField]
    private TMP_Text currentWordText;
    [SerializeField]
    private TMP_Text currentWordValueText;
    public Button sellButton;

    public WordManager wordManager;
    public PlantManager plantManager;

    void Start()
    {
        //To deactivate any existing plants in the field
        ResetPlant();
        //Debug.Log(level);
    }

    public void ResetPlant()
    {
        isEmpty = true;
        level = 0;
        wordValue = 0;
        rootWord = string.Empty;
        currentWord = string.Empty;
        gameObject.SetActive(false);
    }
    public bool IsEmpty()
    {
        return isEmpty;
    }

    //Pass info from plant manager
    public void PlantWord(string firstWord)
    {
        //Adjusting the internal values of the plant
        isEmpty = false;
        rootWord= firstWord;
        level = 0;
        wordValue += 100;
        currentWord= firstWord;

        //Adjusting the UI of the plant
        UpdatePlantUI();
    }

    public bool GrowWord(string firstWord, int type) 
    {
        string newWord = string.Empty;
        //Check if the new word is correct
        newWord = wordManager.MixWords(currentWord, firstWord, type);

        //If the new word is correct adjust the plant values
        if(wordManager.CheckWord(newWord))
        {
            //Pass new info to the plant 
            level += 1;
            wordValue += 100;
            currentWord = newWord;

            UpdatePlantUI();
            return true;
        }
        
        return false;
    }

    public void HarvestWord()
    {
        plantManager.SellPlant(wordValue);
        ResetPlant();
    }

    public void UpdatePlantUI()
    {
        gameObject.SetActive(true);
        currentWordText.text = currentWord;
        currentWordValueText.text = wordValue.ToString();
    }

    public void DisableSellButton()
    {
        sellButton.interactable = false;
    }

    public void EnableSellButton()
    {
        sellButton.interactable = true;
    }
}
