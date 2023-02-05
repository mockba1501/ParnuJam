using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    public Transform itemsOnSale;

    public Image seedBagImg;
    public Image fertilizerBagImg;
    ItemSlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsOnSale.GetComponentsInChildren<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void InitilizeWordSlots()
    {

    }
}
