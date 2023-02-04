using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Initialize the game with a list all possible scriptable object
    public List<RootWord> words;
    public Queue<Tuple<string, wordTypes>> wordsQueue;
    public enum wordTypes
    {
        Root,
        Prefix,
        Suffix
    }
    // Start is called before the first frame update
    void Start()
    {
        //When you start the game you need to populate information from the scriptable data to formulate the words
        //Create a queue structure to include the words 
        PopulateWordOptions();

    //    string root = words[0].rootWord.ToString();
    //   Debug.Log(root);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateWordOptions()
    {
        int countRoots = words.Count;
        //int countGeneratedWords = 0;

        Debug.Log(countRoots);
        
        //string root = words[0].rootWord.ToString();
        //Generate around 20 words
        
        for (int i =0; i<countRoots; i++) 
        {
            wordsQueue.Enqueue(new Tuple<string, wordTypes>( words[i].rootWord.ToString(), 0));
            
            
        }
        
    }
}
