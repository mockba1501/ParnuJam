using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    //This should store the word item that will be added to the Item slot to keep track of the incoming words

    public static Inventory Instance { get; private set; }
    public int size = 6;
    public List<WordItem> wordItems = new List<WordItem>();
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        Instance = this;
    }

    //We need to store the word info in the inventory list 
    //Utility function to add/remove from the inventory

    public bool Add(WordItem newWord)
    {
        if (wordItems.Count < size)
        {
            wordItems.Add(newWord);
            return true;
        }
        else
        {
            Debug.Log("You can't add more words to the inventory");
            return false;
        }
    }

    public bool Add(string word, int type)
    {
        if (wordItems.Count < size)
        {
            wordItems.Add(new WordItem(word, type));
            return true;
        }
        else
        {
            Debug.Log("You can't add more words to the inventory");
            return false;
        }
    }

    public void Remove(WordItem wordItem)
    {
        //Delete the word item from the list
        wordItems.Remove(wordItem);
    }
}