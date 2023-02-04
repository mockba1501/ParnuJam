using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Root Word")]
public class RootWord : ScriptableObject
{
    [SerializeField]
    private string rootWord;

    [SerializeField]
    private List<string> prefixList;

    [SerializeField]
    private List<string> suffixList;

    [SerializeField]
    private List<string> possibleSolutions;
}
