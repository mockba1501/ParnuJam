using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Initialize the game with a list all possible scriptable object
    //[SerializeField]
    public List<RootWord> words;

    // Start is called before the first frame update
    void Start()
    {
        //When you start the game you need to populate information from the scriptable data to formulate the words
        //Create a queue structure to include the words 

    //    string root = words[0].rootWord.ToString();
     //   Debug.Log(root);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
