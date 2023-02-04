using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    public enum wordTypes
    {
        Root,
        Prefix,
        Suffix
    }
    public Image icon;
    public Button removeButton;
    public Text wordLabel;
    public int type; //0 root, 1 prefix, 2 suffix

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
