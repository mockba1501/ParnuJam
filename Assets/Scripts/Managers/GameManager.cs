using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Game Resources & Stats
    [SerializeField]
    private int money;
    [SerializeField]
    private int wordsGeneratedCounter;

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
}
