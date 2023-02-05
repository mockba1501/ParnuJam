using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Initialize the game with a list all possible scriptable object
    public List<RootWord> words;
    //private Queue<Tuple<string, WordTypes>> wordsQueue = new Queue<Tuple<string, WordTypes>>();
    private Queue<WordItem> wordsQueue = new Queue<WordItem>();
    private List<string> correctSolutions= new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //When you start the game you need to populate information from the scriptable data to formulate the words
        //Create a queue structure to include the words 
        PopulateWordOptions();
        //PrintWords();
        GenerateWordSolutions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateWordSolutions()
    {
        foreach(var word in words) 
        {
            correctSolutions.AddRange(word.possibleSolutions);
        }
    }

    public bool CheckWord(string word)
    {
        return correctSolutions.Contains(word);
    }

    private void PopulateWordOptions()
    {
        int totalRoots = words.Count;
        int countRoots = 0;
        int countGeneratedWords = 0;

        //Stop after generating 20 roots or after passing over all roots
        while (countGeneratedWords < 20  && countRoots <totalRoots)
        {
            //Add the root word
            string tmp = words[countRoots].rootWord.ToString();
            WordItem tmpRootWord = new WordItem(tmp,0); //Level 0 stands for Root words

            //wordsQueue.Enqueue(new Tuple<string, WordTypes>(tmp, WordTypes.Root));
            wordsQueue.Enqueue(tmpRootWord);

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

                    WordItem tmpPrefixWord = new WordItem(tmp, 1); //Level 1 stands for Prefix words
                    //wordsQueue.Enqueue(new Tuple<string, WordTypes>(tmp, WordTypes.Prefix));
                    wordsQueue.Enqueue(tmpPrefixWord);

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

                    WordItem tmpSuffixWord = new WordItem(tmp, 2); //Level 2 stands for Prefix words
                    wordsQueue.Enqueue(tmpSuffixWord);
                    // wordsQueue.Enqueue(new Tuple<string, WordTypes>(tmp, WordTypes.Suffix));
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
            Debug.Log($"Welcome word {i} {data.word} of type {data.type}");
            i++;
        }
    }

    
    public WordItem GetWord()
    {
        WordItem data = wordsQueue.Dequeue();
        return data;
        //Access the invenotry slots using the Add function 
        //Pass the wordItems (word and the type)
    }


}
