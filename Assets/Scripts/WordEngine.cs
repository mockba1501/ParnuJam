using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordEngine : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        SelectRandomWords();
    }

    
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
    List<string> finalWords = new List<string>
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
        return true;
    }
    public void SelectRandomWords()
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
    }
  //  GetRootWords()


}
