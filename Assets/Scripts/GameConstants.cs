using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WordItem
{
    public string word;
    public int type;

    
    public WordItem(string w, int t)
    {
        word = w;
        type = t;
    }
}

public enum WordTypes
{
    Root,
    Prefix,
    Suffix
}