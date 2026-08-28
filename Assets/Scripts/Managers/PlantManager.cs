using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/*
    //General functions to check available planting slots [Done]
    //  - Highlight an empty space 
    //  - Select an existing plant slot

    //Initialize the field with the children of the current PlantPos

    //Create functions for:
    //  - associating a seed word with an empty spot [Done] EnablePlant
    //  - associate a fertilizer with an existing seed/constructed word

    //If a player chooses to sell the vegetable
    //  a) Reset all the values of the prefab (make default values)
    //  b) Disable the sprite 
 */

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }
    public static event Action<int> OnPlantSold;
    public static event Action<string> OnPlantInstructionRequested;
    
    public List<PlantStatus> plantPos;
    public List<Outline> plantsOutline;
    public TMP_Text coinText;
    public int seedCost;
    public int fertilizerCost;

    [SerializeField]
    private int plantSpotsCurrentCount;
    [SerializeField]
    private int plantSpotsCountMax;
    [SerializeField]
    private WordItem currentWord;

    private PlantStatus selectedPlant;
    private bool isFertilizing;
    private Transform highlightedPlant;
    private InputSystem_Actions inputSystemActions;


    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }
    
    private void OnDestroy()
    {
        inputSystemActions.Dispose();
    }
    private void OnEnable()
    {
        inputSystemActions.UI.Click.performed += OnClick;
        inputSystemActions.UI.Point.performed += OnMouseMove;
        inputSystemActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputSystemActions.UI.Click.performed -= OnClick;
        inputSystemActions.UI.Point.performed -= OnMouseMove;
        inputSystemActions.UI.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Initialize the count to zero
        plantSpotsCurrentCount = 0;

        //Count the number of available spots
        plantSpotsCountMax = plantPos.Count;

        seedCost = 50;
        fertilizerCost = 50;

        //Once you start the program this is set to false
        isFertilizing = false;
        selectedPlant = null;

        DisableOutline();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && isFertilizing)
        {
            HandlePlantSelection();
        }
    }

    // NEW: Handle mouse movement for highlighting
    private void OnMouseMove(InputAction.CallbackContext context)
    {
        if (isFertilizing)
        {
            HandlePlantHighlighting();
        }
    }

    private void HandlePlantSelection()
    {
        Vector2 pointPosition = inputSystemActions.UI.Point.ReadValue<Vector2>(); 
        Ray ray = Camera.main.ScreenPointToRay(pointPosition);
        RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit2d.collider&&  hit2d.transform )
        {
            GameObject hitObject = hit2d.transform.gameObject;
            if (hitObject && hitObject.CompareTag("Carrot"))
            {
                selectedPlant = hitObject.GetComponent<PlantStatus>();
                isFertilizing = false;
                DisableOutline();

                //Pass the word item to the selected word, if it didn't work out
                if (!selectedPlant.GrowWord(currentWord.word, currentWord.type))
                {
                    OnPlantInstructionRequested?.Invoke("Ops! Incorrect Fertilizer Combination!");
                    
                    if (SFXManager.Instance)
                        SFXManager.Instance.ManageSFX(4);
                    else
                    {
                        Debug.Log("SFX Manager not found!");
                    }
                }
                else
                {
                    if (SFXManager.Instance)
                        SFXManager.Instance.ManageSFX(1);
                    else
                    {
                        Debug.Log("SFX Manager not found!");
                    }

                    OnPlantInstructionRequested?.Invoke("Congratulations Correct Mix!");
                    Invoke("SellOrFertilizeMessage", 1);
                    UpdateWordDisplay();
                }
            }
            GameManager.Instance.CheckWinningCondition();
        }
    }

    private void HandlePlantHighlighting()
    {
        if (highlightedPlant)
        {
            highlightedPlant.gameObject.GetComponentInChildren<Outline>().OutlineColor = Color.white;
            highlightedPlant = null;
        }

        Vector2 mousePosition = inputSystemActions.UI.Point.ReadValue<Vector2>(); 
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit2d.collider && hit2d.transform.gameObject.tag == "Carrot")
        {
            highlightedPlant = hit2d.transform;
            highlightedPlant.gameObject.GetComponentInChildren<Outline>().OutlineColor = Color.red;
        }
    }
    
    private void ActivateOutline()
    {
        foreach (var plant in plantsOutline)
        {
            plant.enabled = true;
        }
    }

    private void DisableOutline()
    {
        foreach (var plant in plantsOutline)
        {
            plant.enabled = false;
        }    
    }

    public void ActivateButtons()
    {
        foreach (var plant in plantPos)
        {
            plant.EnableSellButton();
        }
    }

    public void DisableButtons()
    {
        foreach (var plant in plantPos)
        {
            plant.DisableSellButton();
        }
    }

    //Pass the word item info to the plant
    public void EnablePlant(int pos,string word)
    {
        //Debug.Log($"Enabling the plant {word} at position {pos}");

        //Increment by 1
        plantSpotsCurrentCount++;

        plantPos[pos].PlantWord(word);

        UpdateMoney(-seedCost);
        UpdateWordDisplay();

    }
    public void UpdateWordDisplay()
    {
        GameManager.Instance.IncrementWordCount();
        UIManager.Instance.UpdatedWordsGeneratedCounter();
    }

    public void UpdateMoney(int amount)
    {
        GameManager.Instance.ModifyMoney(amount);
        //UIManager.Instance.UpdateCoinsDisplay();
    }

    //Check the spots and return if there is an empty space or not
    private bool IsFree()
    {
        //If the current filled spots is equal to the maximum then it is full
        return plantSpotsCurrentCount < plantSpotsCountMax;
    }    

    public bool IsEmpty()
    {
        return plantSpotsCurrentCount== 0;
    }

    public bool IsFertilizing()
    {
        return isFertilizing;
    }

    public int FreeSpot()
    { 
        int index = -1; // -1 indicates no free spots as well

        //Return the index of an empty spot
        if(IsFree())
        {
            for (int i = 0; i < plantSpotsCountMax; i++)
            {
                if (plantPos[i].IsEmpty())
                {
                    index = i;
                    break;
                }
            }
        }
        Debug.Log($"Spot {index} is free");
        return index;
    }    

    public bool PlantRoot(string word)
    {
        if (!isFertilizing)
        {
            // Check if there are free spots and enough money
            if (GameManager.Instance.IsMoneySufficient())
            {
                if (IsFree())
                {
                    //Retrieve an empty spot, pass the word info to plant
                    EnablePlant(FreeSpot(), word);
                    OnPlantInstructionRequested?.Invoke("Congratulations you planted a new seed!");
                    Invoke("FertilizeMessage", 1);
                    return true;
                }
                else
                {
                    OnPlantInstructionRequested?.Invoke("No empty slots!");
                    return false;
                }
            }
            else
            {
                OnPlantInstructionRequested?.Invoke("Not enough money!");
                return false;
            }
        }
        else
        {
            OnPlantInstructionRequested?.Invoke("Finish Fertilization task first!");
            return false;
        }
    }

    public bool ApplyFertilizer(WordItem receivedWord)
    {
        //if a correct combination return success else if incorrect combination return false
        //Success
        ActivateOutline();
        //The selected item is a fertilizer
        //  1) you select a correct root combination it will grow to the following level
        //  2) if incorrect root nothing will happen
        // Check if there are free spots and enough money
        if (!isFertilizing)
        {
            if (GameManager.Instance.IsMoneySufficient())
            {
                if (!IsEmpty())
                {
                    currentWord = receivedWord;
                    isFertilizing = true;
                    OnPlantInstructionRequested?.Invoke($"Click on the carrot to apply the {currentWord.word} fertilizer");
                    UpdateMoney(-fertilizerCost);
                    return true;
                }
                else
                {
                    OnPlantInstructionRequested?.Invoke("Empty field, plant some roots first!");
                    return false;
                }
            }
            else
            {
                OnPlantInstructionRequested?.Invoke("Not enough money!");
                return false;
            }
        }
        else
        {
            OnPlantInstructionRequested?.Invoke("Finish previous fertilization task first!");
            return false;
        }
    }

    public bool SellPlant(int value)
    {
        if(isFertilizing)
        {
            OnPlantInstructionRequested?.Invoke("Finish previous fertilization task first!");
            return false;
        }

        //Decrement by 1
        plantSpotsCurrentCount--;
        OnPlantSold?.Invoke(value);

        return true;
    }

    public void PlantSeedMessage()
    {
        OnPlantInstructionRequested?.Invoke("Choose a seed to plant!");
    }

    public void SellOrFertilizeMessage()
    {
        OnPlantInstructionRequested?.Invoke("You can sell the plant or use a fertilizer to grow your plant bigger!");
    }

    public void FertilizeMessage()
    {
        OnPlantInstructionRequested?.Invoke("Use a fertilizer to grow your plant!");
    }
}
