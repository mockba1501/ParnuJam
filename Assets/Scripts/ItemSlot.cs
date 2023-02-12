using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiMngr;
    public PlantManager plantManager;
    
    public Image icon; // Previsously Image class
    public Button removeButton;
    public Button useButton;
    public TMP_Text buttonText;
    public int removeCost = -50;

    //word item type values: //0 root, 1 prefix, 2 suffix
    public WordItem wordItem;

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
    //1) If it was a root word and there are empty spaces in the field plant it [DONE]

    //2) If it was a fertilizer:
        a) you select a correct root combination it will grow to the following level
        b) if incorrect root nothing will happen

    //3) Clear the slot and add a new word in its place     [DONE]
    //We need to invoke a call on change to push a new word 
    //Refresh all other words in the future list

    //4) If the words are finished (less than 3 words remain in the list)
    //  a) Either generate new list of words
    //  b) Or destroy/don't show the words

    //Also if you click the remove button you can delete the word for a cost [DONE]

*/
    //When you click on an item either (Root or Fertilizer)
    public void UseItem()
    {
        //The selected item is a root
        if(this.wordItem.type == 0)
        {
            //Check if there are free spots or not
            if (plantManager.PlantRoot(wordItem.word))
            { 
                ClearSlot();
            }
            else
            {                
                //Send a message to the user there are not empty spots! 
                //uiMngr.UpdateInstructionMessage("No Free Slots!");
                //Do nothing
            }
        }
        else
        {
            //uiMngr.UpdateInstructionMessage("Can't use now!");
           
            if(plantManager.ApplyFertilizer(wordItem)) 
            {

                ClearSlot();
            }
        }

    }

    public void RemoveItem() 
    {
        if(gameManager.IsMoneySufficient())
        {
            gameManager.ModifyMoney(removeCost);
            uiMngr.UpdateCoinsDisplay();
            ClearSlot();
        }
        else
        {
            uiMngr.UpdateInstructionMessage("Can't remove item, not enough money!");
        }
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
