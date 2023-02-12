using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{   
    public Image icon; // Previsously Image class
    //public Button removeButton;
    public Button useButton;
    public TMP_Text buttonText;

    //word item type values: //0 root, 1 prefix, 2 suffix
    public WordItem wordItem;

    public UIManager uiMngr;
    public PlantManager plantManager;

    //Adding a new word to the item slot
    public void AddItem(WordItem newItem)
    {
        this.wordItem = newItem;
       
        buttonText.text = wordItem.word;

        // type 0 is root/seed
        if (this.wordItem.type == 0)
        {
             icon.sprite = uiMngr.seedBagImg;
        } 
        //other types 1,2 are suffix and prefix
        else
        {
            icon.sprite = uiMngr.fertilizerBagImg;
        }
    }

    /*
    //When you click on an item either two things happen:
    //1) If it was a root word and there are empty spaces in the field plant it

    //2) If it was a fertilizer:
        a) you select a correct root combination it will grow to the following level
        b) if incorrect root nothing will happen

    //3) Clear the slot and add a new word in its place     [DONE]
    //We need to invoke a call on change to push a new word 
    //Refresh all other words in the future list

    //4) If the words are finished (less than 3 words remain in the list)
    //  a) Either generate new list of words
    //  b) Or destroy/don't show the words

*/
    //When you click on an item either (Root or Fertilizer)
    public void UseItem()
    {
        //The selected item is a root
        if(this.wordItem.type == 0)
        {
            //Check if there are free spots or not
            if (plantManager.IsFree())
            {
                //Retrieve an empty spot, pass the word info to plant
                plantManager.EnablePlant(plantManager.FreeSpot());

                ClearSlot();
            }
            else
            {
                //Send a message to the user there are not empty spots! 
                //Do nothing
            }
        }
        else
        {
            //The selected item is a fertilizer
            //  a) you select a correct root combination it will grow to the following level
            //  b) if incorrect root nothing will happen
        }

        

        //AddNextWord();

    }

    public void ClearSlot()
    {
        this.gameObject.SetActive(false);
        Invoke("ResetSlot", 1);
        
    }

    public void ResetSlot()
    {
        uiMngr.RefreshSlot(this);
        this.gameObject.SetActive(true);
        uiMngr.GetNextWords();
    }
}
