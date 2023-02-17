using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlantManager plantManager;
    public UIManager uiManager;

    //Game Resources & Stats
    [SerializeField]
    private int money;
    [SerializeField]
    private int wordsGeneratedCounter;

    public bool isGameOver;

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
        money = 1000;
        wordsGeneratedCounter = 0;
    }

    // Update is called once per frame
  /*  void Update()
    {
        //Winning Conditions:
        // - Check the number of generated words
        // - Check the available money
    }
  */
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

    public bool IsMoneySufficient()
    {
        return money >= 50;
    }

    public void CheckWinningCondition() 
    {
        //IF there are no more slots with root words and there are no current plants
        if(!uiManager.IsRootAvailable() && plantManager.IsEmpty())
        {
            isGameOver = true;
            Debug.Log("Game Over: No roots reamining");
        }

        //No money left and no roots available in the field
        if(money < 50 && plantManager.IsEmpty())
        { 
            isGameOver = true;
            Debug.Log("Game Over: No money left");
        }

        //Reached the end of the available words in the shop

    }
}
