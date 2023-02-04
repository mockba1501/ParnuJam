using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Words/Root Word")]
public class RootWord : ScriptableObject
{
    public string rootWord;

    public List<string> prefixList;

    public List<string> suffixList;

    public List<string> possibleSolutions;
}
