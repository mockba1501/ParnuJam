using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordEngine : MonoBehaviour
{
    public RootWord word1, word2;

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++) 
        {
            CheckCorrect(SelectRandomWords());
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        CheckCorrect(SelectRandomWords());
    }
    */
    
    //Declaration of different words
    List<string> rootWords = new List<string>
    {
        "cent","create","multi"
    };

    List<string> prefix = new List<string>
    {
        "bi","per","pro","re"
    };

    List<string> suffix= new List<string>
    {
        "ennial","age","ury"
    };
    public List<string> finalWords = new List<string>
    {
        "centennial", "bicentennial","percent","percentage","century","recreate","recreation","recreational",
        "procreate","procreative"
    };

    public List<string> GetRootWords()
    {
        return rootWords;
    }
    public List<string> GetPrefix()
    {
        return prefix;
    }

    public List<string> GetSuffix()
    {
        return suffix;
    }

    public bool CheckCorrect(string answer)
    {
        //check if the word exist or not in the choices
        if (finalWords.Contains(answer))
        {
            Debug.Log(answer + " is a correct word Congratulations");
            return true;
        }
        else
            return false;
        
    }
    public string SelectRandomWords()
    {
        List<string> roots = GetRootWords();
        List<string> suffix = GetSuffix();
        List<string> prefix = GetPrefix();
        //List<string> 
        string root = roots[Random.Range(0, roots.Count)];
        string newWord;
        if (Random.Range(0, 1) % 2 == 0)
        {
            Debug.Log("Even choice...");
            newWord = prefix[Random.Range(0, prefix.Count)] + root;
        }
        else
        {
            Debug.Log("Odd choice...");
            newWord = root + suffix[Random.Range(0, suffix.Count)];
        }

        Debug.Log(newWord);
        return newWord;
    }
  //  GetRootWords()


}
