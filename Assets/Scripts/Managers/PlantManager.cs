using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{

    //Initialize the field with the children of the current PlantPos


    //Create functions for:
    // - associating a seed word with an empty spot

    // - associate a fertilizer with an existing seed/constructed word


    //If a player chooses to sell the vegetable
    //  a) Reset all the values of the prefab (make default values)
    //  b) Disable the sprite

    //Add points to the game 

    public GameObject plantParent;
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
