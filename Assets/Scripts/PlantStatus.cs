using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlantStatus : MonoBehaviour
{
    public string rootWord;
    public bool isEmpty;
    public int level;
    public int wordValue;
    public GameObject carrotPrefab;
    public string currentWord;

    [SerializeField]
    private TMP_Text currentWordText;
    [SerializeField]
    private TMP_Text currentWordValueText;

    void Start()
    {
        //To deactivate any existing plants in the field
        ResetPlant();
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
        gameObject.SetActive(true);
        currentWordText.text = currentWord;
        currentWordValueText.text = wordValue.ToString();
    }

     
}
