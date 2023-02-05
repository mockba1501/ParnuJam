using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Initialize the game with a list all possible scriptable object
    public List<RootWord> words;
    public Queue<Tuple<string, wordTypes>> wordsQueue = new Queue<Tuple<string, wordTypes>>();
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
        //PrintWords();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateWordOptions()
    {
        int totalRoots = words.Count;
        int countRoots = 0;
        int countGeneratedWords = 0;

        //Stop after generating 20 roots or after passing over all roots
        while (countGeneratedWords < 20  && countRoots <totalRoots)
        {
            //Add the root word
            string tmp = words[countRoots].rootWord.ToString();
            wordsQueue.Enqueue(new Tuple<string, wordTypes>(tmp, wordTypes.Root));
            countGeneratedWords++;
            Debug.Log($"Word {countGeneratedWords} Adding root {tmp}");
            //Extract the prefix words
            List<string> tmpPrefix = new List<string>(words[countRoots].prefixList);
            int tmpPrefixCount = tmpPrefix.Count;
            int prefixCount = 0;
            
            //Make sure that the list actually contains some words
            if (tmpPrefixCount > 0)
            {
                //Loop until you reach half the items or a max of two items
                while (/*prefixCount <= tmpPrefixCount / 2 && */ prefixCount < 1)
                {
                    int randomIndex = Random.Range(0, tmpPrefix.Count);
                    tmp = tmpPrefix[randomIndex];

                    wordsQueue.Enqueue(new Tuple<string, wordTypes>(tmp, wordTypes.Prefix));
                    tmpPrefix.RemoveAt(randomIndex);
                    
                    countGeneratedWords++;
                    prefixCount++;
                    Debug.Log($"Word {countGeneratedWords} Adding prefix {tmp}");
                }
            }

            //Extract and add the suffix words
            List<string> tmpSuffix = new List<string>(words[countRoots].suffixList);
            int tmpSuffixCount = tmpSuffix.Count;
            int suffixCount = 0;


            //Make sure that the list actually contains some words
            if (tmpSuffixCount > 0)
            {
                //Loop until you reach half the items or a max of two items
                while (suffixCount <= tmpSuffixCount / 2 && suffixCount < 3)
                {
                    int randomIndex = Random.Range(0, tmpSuffix.Count);
                    tmp = tmpSuffix[randomIndex];

                    wordsQueue.Enqueue(new Tuple<string, wordTypes>(tmp, wordTypes.Suffix));
                    tmpSuffix.RemoveAt(randomIndex);
                    
                    countGeneratedWords++;
                    suffixCount++;
                    Debug.Log($"Word {countGeneratedWords} Adding suffix {tmp}");

                }
            }
            countRoots++;
        }//end of word generation
        
    }

    public void PrintWords()
    {
        int i = 0;
        //foreach(string in wordsQueue)
        foreach(var data in wordsQueue)
        { 
            Debug.Log($"word {i} {data.Item1} of type {data.Item2}");
            i++;
        }
    }

}
