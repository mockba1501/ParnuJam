using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{
    //General functions to check available planting slots [Done]
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
    public List<PlantStatus> plantPos;
    public TMP_Text coinText;

    [SerializeField]
    private int plantSpotsCurrentCount;
    [SerializeField]
    private int plantSpotsCountMax;

    public static PlantManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the count to zero
        plantSpotsCurrentCount = 0;

        //Count the number of available spots
        plantSpotsCountMax = plantPos.Count;

    }

    //Pass the word item info to the plant
    public void EnablePlant(int pos,string word)
    {
        Debug.Log($"Enabling the plant {word} at position {pos}");
        int money;
        string moneyString;

        //Increment by 1
        plantSpotsCurrentCount++;

        money = gameManagerIntance.modifyMoney(0);
        moneyString = coinText.text;
        money = Int32.Parse(moneyString);

       // if(pos < plantPos.Count && money >= 50) 
       // {
            plantPos[pos].PlantWord(word);
          //  money -= 50;
        //}

        coinText.text = money.ToString();
    }

    //Check the spots and return if there is an empty space or not
    public bool IsFree()
    {
        //If the current filled spots is equal to the maximum then it is full
        return plantSpotsCurrentCount < plantSpotsCountMax;
    }    
    public int FreeSpot()
    { 
        int index = -1; // -1 indicates no free spots as well

        //Return the index of an empty spot
        if(IsFree())
        {
            for (int i = 0; i < plantSpotsCountMax; i++)
            {
                if (plantPos[i].IsEmpty())
                {
                    index = i;
                    break;
                }
            }
        }
        Debug.Log($"Spot {index} is free");
        return index; 
    }    
}
