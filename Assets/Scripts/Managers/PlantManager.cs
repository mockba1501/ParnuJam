using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{
    //General functions to check available planting slots
    //Highlight an empty space 
    //Select an exsiting plant slot

    //Initialize the field with the children of the current PlantPos


    //Create functions for:
    // - associating a seed word with an empty spot

    // - associate a fertilizer with an existing seed/constructed word


    //If a player chooses to sell the vegetable
    //  a) Reset all the values of the prefab (make default values)
    //  b) Disable the sprite
    public GameManager gameManagerIntance;

    //public GameObject plantParent;
    public List<GameObject> plantPos;
    public TMP_Text coinText;

    public static PlantManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<plantPos.Count; i++) 
        {
            plantPos[i].gameObject.SetActive(false);
        }


    }

    public void EnablePlant(int pos)
    {
        int money;
        string moneyString;

        money = gameManagerIntance.modifyMoney(0);
        moneyString = coinText.text;
        money = Int32.Parse(moneyString);

        if(pos < plantPos.Count && money >= 50) 
        {
            plantPos[pos].gameObject.SetActive(true);
            money -= 50;
        }

        coinText.text = money.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
