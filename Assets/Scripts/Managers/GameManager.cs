using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlantManager plantManager;
    public UIManager uiManager;
    public WordManager wordManager;

    //Game Resources & Stats
    [SerializeField]
    private int money;
    [SerializeField]
    private int wordsGeneratedCounter;

    private int wordWinningTarget;
    public bool isGameOver;
    public bool isWin;

    void Awake()
    {
        //Debug.Log("Accessing Game Manager Awake " + System.DateTime.Now.Month.ToString());

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isWin = false;
        money = 1000;
        wordsGeneratedCounter = 0;
        wordWinningTarget = wordManager.GetStemCount()/2 + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            DisableGameButtons();
            if (isWin)
            {
                uiManager.UpdateInstructionMessage("Game Over: You Win");
            }
            else
            {
                uiManager.UpdateInstructionMessage("Game Over: You Lose");
            }
            
        }
        //Winning Conditions:
        // - Check the number of generated words
        // - Check the available money
    }

    public void ModifyMoney(int amount)
    {
        money += amount;
    }

    public void IncrementWordCount() 
    {
        wordsGeneratedCounter++;
    }

    public int CurrentMoney()
    { 
        return money; 
    }

    public int CurrentWordsGeneratedCounter() 
    {
        return wordsGeneratedCounter;
    }

    public int GetWordWinningTarget()
    {
        return wordWinningTarget;
    }
    public bool IsMoneySufficient()
    {
        return money >= 50;
    }

    public void CheckWinningCondition() 
    {
        if (wordsGeneratedCounter == wordWinningTarget)
        {
            isGameOver = true;
            isWin = true;
            uiManager.UpdateInstructionMessage("Congratulations You Won");
            uiManager.UpdateGameOverMessage("Congratulations You Won");
        }
        else
        //IF there are no more slots with root words and there are no current plants
        if (!uiManager.IsRootAvailable() && plantManager.IsEmpty() && wordManager.IsEmpty())
        {
            isGameOver = true;
            Debug.Log("Game Over: No roots reamining");
            uiManager.UpdateInstructionMessage("Game Over: No roots reamining");
            uiManager.UpdateGameOverMessage("Game Over: No roots reamining");
        }
        else
        //No money left and no roots available in the field
        if(money < 50 && plantManager.IsEmpty())
        { 
            isGameOver = true;
            Debug.Log("Game Over: No money left");
            uiManager.UpdateInstructionMessage("Game Over: No money left");
            uiManager.UpdateGameOverMessage("Game Over: No money left");
        }
        else
        //Reached the end of the available words in the shop
        if(wordManager.IsEmpty() && uiManager.IsSlotsEmpty())
        {
            isGameOver = true;
            Debug.Log("Game Over: No word stems left");
            uiManager.UpdateInstructionMessage("Game Over: No word stems left");
            uiManager.UpdateGameOverMessage("Game Over: No word stems left");

        }

    }

    public void DisableGameButtons()
    {
        uiManager.DisableSlots();
        plantManager.DisableButtons();
    }

    public void EnableGameButtons()
    {
        uiManager.ActivateSlots();
        plantManager.ActivateButtons();
    }
}
