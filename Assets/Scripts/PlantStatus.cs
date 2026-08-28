using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantStatus : MonoBehaviour
{
    public event System.Action<int> OnWordGrown;
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

    void Start()
    {
        //To deactivate any existing plants in the field
        ResetPlant();
        //Debug.Log(level);
    }

    private void ResetPlant()
    {
        isEmpty = true;
        level = 0;
        wordValue = 0;
        rootWord = string.Empty;
        currentWord = string.Empty;
        gameObject.SetActive(false);
        OnWordGrown?.Invoke(level);
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
        OnWordGrown?.Invoke(level);
    }

    public bool GrowWord(string firstWord, int type) 
    {
        string newWord = string.Empty;
        //Check if the new word is correct
        newWord = WordManager.Instance.MixWords(currentWord, firstWord, type);

        //If the new word is correct adjust the plant values
        if(WordManager.Instance.CheckWord(newWord))
        {
            //Pass new info to the plant 
            level += 1;
            wordValue += 100;
            currentWord = newWord;

            UpdatePlantUI();
            OnWordGrown?.Invoke(level);
            return true;
        }
        
        return false;
    }

    public void HarvestWord()
    {
        //if successfully sold the plant then remove it
        if(PlantManager.Instance.SellPlant(wordValue))
            ResetPlant();
    }

    private void UpdatePlantUI()
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
