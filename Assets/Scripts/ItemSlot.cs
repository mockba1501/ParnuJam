using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;
    public Text wordLabel;
    public int type; //0 root, 1 prefix, 2 suffix
    public WordItem wordItem;
    public void AddItem(WordItem newItem)
    {
        this.wordItem = newItem;
        
        if(newItem.type == 0)
        {

        }
    }
}
