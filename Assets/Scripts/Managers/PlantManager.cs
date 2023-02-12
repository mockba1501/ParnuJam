using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    //General functions to check available planting slots [Done]
    //  - Highlight an empty space 
    //  - Select an exsiting plant slot

    //Initialize the field with the children of the current PlantPos

    //Create functions for:
    //  - associating a seed word with an empty spot [Done] EnablePlant
    //  - associate a fertilizer with an existing seed/constructed word

    //If a player chooses to sell the vegetable
    //  a) Reset all the values of the prefab (make default values)
    //  b) Disable the sprite 
 */

public class PlantManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiMngr;

    //public GameObject plantParent;
    public List<PlantStatus> plantPos;
    public TMP_Text coinText;
    public int seedCost;

    [SerializeField]
    private int plantSpotsCurrentCount;
    [SerializeField]
    private int plantSpotsCountMax;
    [SerializeField]
    private WordItem currentWord;

    private PlantStatus selectedPlant;
    private bool isFertilizing;

    public static PlantManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the count to zero
        plantSpotsCurrentCount = 0;

        //Count the number of available spots
        plantSpotsCountMax = plantPos.Count;

        seedCost = -50;

        //Once you start the program this is set to false
        isFertilizing = false;
        selectedPlant = null;
    }

    private void Update()
    {
        //If the fertilizing flag is set to true check the ray cast and assign an object
        if(isFertilizing) 
        {
            if(SearchingForPlant())
            {
                //Pass the word item
                //Check if the new word is correct
                //Pass new info to the plant and adjust values
            }
        }

    }

    public bool SearchingForPlant()
    {
        if (Input.GetMouseButtonDown(0))// When clicked Mouse-Left-Button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit2d.collider != null)
            {
                if(hit2d.transform.gameObject.tag == "Carrot")
                {
                    Debug.Log("clicked: " + hit2d.transform.gameObject.name);
                    selectedPlant = hit2d.transform.gameObject.GetComponent<PlantStatus>();
                    isFertilizing = false;
                    return true;
                }
            }
        }
        return false;
    }

    //Pass the word item info to the plant
    public void EnablePlant(int pos,string word)
    {
        Debug.Log($"Enabling the plant {word} at position {pos}");

        //Increment by 1
        plantSpotsCurrentCount++;

        plantPos[pos].PlantWord(word);

        gameManager.ModifyMoney(seedCost);
        uiMngr.UpdateCoinsDisplay();

    }

    //Check the spots and return if there is an empty space or not
    public bool IsFree()
    {
        //If the current filled spots is equal to the maximum then it is full
        return plantSpotsCurrentCount < plantSpotsCountMax;
    }    

    public bool IsEmpty()
    {
        return plantSpotsCurrentCount== 0;
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
        if (!isFertilizing)
        {
            // Check if there are free spots and enough money
            if (gameManager.IsMoneySufficient())
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
        else
        {
            uiMngr.UpdateInstructionMessage("Finish Fertilization task first!");
            return false;
        }
    }

    public bool ApplyFertilizer(WordItem recievedWord)
    {
        //if a correct combination return success else if incorrect combination return false
        //Success

        //The selected item is a fertilizer
        //  a) you select a correct root combination it will grow to the following level
        //  b) if incorrect root nothing will happen
        // Check if there are free spots and enough money
        if (!isFertilizing)
        {
            if (gameManager.IsMoneySufficient())
            {
                if (!IsEmpty())
                {
                    isFertilizing = true;
                    currentWord = recievedWord;
                    //Check the selected plant using Ray Cast
                    //if()
                    //EnablePlant(FreeSpot(), word);
                    return true;
                }
                else
                {
                    uiMngr.UpdateInstructionMessage("Empty field, plant some roots first!");
                    return false;
                }
            }
            else
            {
                uiMngr.UpdateInstructionMessage("Not enough money!");
                return false;
            }
        }
        else
        {
            uiMngr.UpdateInstructionMessage("Finish previous fertilization task first!");
            return false;
        }
    }

}
