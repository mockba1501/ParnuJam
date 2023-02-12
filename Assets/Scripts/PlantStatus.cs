using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlantStatus : MonoBehaviour
{
    public string rootWord;
    public bool isEmpty;
    public int level;
    public GameObject carrotPrefab;
    public string currentWord;

    void Start()
    {
        isEmpty= true;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    //Pass info from plant manager
    public void PlantWord()
    {

    }
}
