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
    private int wordsGeneratedCounter = 0;

    void Awake()
    {
        Debug.Log("Accessing Game Manager Awake " + System.DateTime.Now.Month.ToString());

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
        //Debug.Log("Accessing Game Manager Start " + System.DateTime.Now.Month.ToString());


    }

    // Update is called once per frame
    void Update()
    {
        //Winning Conditions:
        // - Check the number of generated words
        // - Check the available money
    }

    public int modifyMoney(int amount)
    {
        return money + amount;
    }

    public int modifyNumOfWords(int amount) 
    {
        return wordsGeneratedCounter + amount;
    }
}
