using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PlantManager : MonoBehaviour
{
    //General functions to check available planting slots [Done]
    //Highlight an empty space 
    //Select an exsiting plant slot

    //Initialize the field with the children of the current PlantPos


    //Create functions for:
    // - associating a seed word with an empty spot [Done] EnablePlant

    // - associate a fertilizer with an existing seed/constructed word


    //If a player chooses to sell the vegetable
    //  a) Reset all the values of the prefab (make default values)
    //  b) Disable the sprite
    public GameManager gameManager;
    public UIManager uiMngr;

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

        //Increment by 1
        plantSpotsCurrentCount++;

        plantPos[pos].PlantWord(word);

        gameManager.ModifyMoney(-50);
        uiMngr.UpdateCoinsDisplay();

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

    public bool PlantRoot(string word)
    {
        // Check if there are free spots and enough money
        if(gameManager.IsMoneySufficient())
        {
            if (IsFree())
            {   
                //Retrieve an empty spot, pass the word info to plant
                EnablePlant(FreeSpot(), word);
                return true;
            }
            else
            {
                uiMngr.UpdateInstructionMessage("No empty slots!");
                return false;
            }
        }
        else
        {
            uiMngr.UpdateInstructionMessage("Not enough money!");
            return false;
        }
    }

    public bool ApplyFertilizer(string word, int type)
    {
        //if a correct combination return success else if incorrect combination return false
        //Success
        return true;
    }

}
