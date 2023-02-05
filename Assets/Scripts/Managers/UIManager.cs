using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    public GameManager gameManager;
    public Transform itemsOnSale;
    public Image seedBagImg; // previously Image class
    public Image fertilizerBagImg; // previously Image class
    ItemSlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsOnSale.GetComponentsInChildren<ItemSlot>();
        InitilizeWordSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void InitilizeWordSlots()
    {
        int i = 0;
        foreach (var slot in slots) 
        {
            i++;
            WordItem tmp = gameManager.GetWord();
            slot.AddItem(tmp);
            Debug.Log($"Word added {i} {tmp.word} of type {tmp.type}");
        }
    }
}
