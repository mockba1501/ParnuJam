using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int> OnMoneyChanged;
    //public PlantManager plantManager;

    //Game Resources & Stats
    [SerializeField]
    private int money;
    [SerializeField]
    private int wordsGeneratedCounter;

    private int wordWinningTarget;
    public bool isGameOver;
    public bool isWin;

    private void OnEnable()
    {
        PlantManager.OnPlantSold += HandlePlantSold;
    }

    private void OnDisable()
    {
        PlantManager.OnPlantSold -= HandlePlantSold;
    }
    
    private void HandlePlantSold(int value)
    {
        ModifyMoney(value);
        CheckWinningCondition();
    }

    void Awake()
    {
        //Debug.Log("Accessing Game Manager Awake " + System.DateTime.Now.Month.ToString());

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isWin = false;
        money = 1000;
        wordsGeneratedCounter = 0;
        
        // Broadcast the initial money to UIManager!
        OnMoneyChanged?.Invoke(money);
        
        // Safety check for wordManager
        if (WordManager.Instance != null)
        {
            wordWinningTarget = WordManager.Instance.GetStemCount()/2 + 1;
        }
        else
        {
            Debug.LogError("GameManager: wordManager is not assigned in the inspector!");
            wordWinningTarget = 0; // Default value
        }
        
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.ManageBGM(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isGameOver)
        {
            DisableGameButtons();
            if (isWin)
            {
                UIManager.Instance.UpdateInstructionMessage("Game Over: You Win");
            }
            else
            {
                UIManager.Instance.UpdateInstructionMessage("Game Over: You Lose");
            }
            
        }
        //Winning Conditions:
        // - Check the number of generated words
        // - Check the available money
        */
    }

    public void ModifyMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void IncrementWordCount() 
    {
        wordsGeneratedCounter++;
    }

    public int CurrentMoney()
    { 
        return money; 
    }

    public int CurrentWordsGeneratedCounter() 
    {
        return wordsGeneratedCounter;
    }

    public int GetWordWinningTarget()
    {
        return wordWinningTarget;
    }
    public bool IsMoneySufficient()
    {
        return money >= 50;
    }

/*
    public void CheckWinningCondition() 
    {
        if(isGameOver)
            return;
        
        if (wordsGeneratedCounter >= wordWinningTarget)
        {
            isGameOver = true;
            isWin = true;
            UIManager.Instance.UpdateInstructionMessage("Congratulations You Won");
            UIManager.Instance.UpdateGameOverMessage("Congratulations You Won");
        }
        else
        //IF there are no more slots with root words and there are no current plants
        if (!UIManager.Instance.IsRootAvailable() && plantManager.IsEmpty() && wordManager.IsEmpty())
        {
            isGameOver = true;
            Debug.Log("Game Over: No roots remaining");
            UIManager.Instance.UpdateInstructionMessage("Game Over: No roots remaining");
            UIManager.Instance.UpdateGameOverMessage("Game Over: No roots remaining");
        }
        else
        //No money left and no roots available in the field
        if(money < 50 && plantManager.IsEmpty())
        { 
            isGameOver = true;
            Debug.Log("Game Over: No money left");
            UIManager.Instance.UpdateInstructionMessage("Game Over: No money left");
            UIManager.Instance.UpdateGameOverMessage("Game Over: No money left");
        }
        else
        //Reached the end of the available words in the shop
        if(wordManager.IsEmpty() && UIManager.Instance.IsSlotsEmpty())
        {
            isGameOver = true;
            Debug.Log("Game Over: No word stems left");
            UIManager.Instance.UpdateInstructionMessage("Game Over: No word stems left");
            UIManager.Instance.UpdateGameOverMessage("Game Over: No word stems left");

        }
    }
*/
    public void CheckWinningCondition() 
    {
        if (isGameOver)
            return;
        
        if (wordsGeneratedCounter >= wordWinningTarget)
        {
            TriggerGameOver(true, "Congratulations You Won");
        }
        else if (UIManager.Instance != null && !UIManager.Instance.IsRootAvailable() && IsFieldEmpty() && IsWordManagerEmpty())
        {
            Debug.Log("Game Over: No roots remaining");
            TriggerGameOver(false, "Game Over: No roots remaining");
        }
        else if (money < 50 && IsFieldEmpty())
        { 
            Debug.Log("Game Over: No money left");
            TriggerGameOver(false, "Game Over: No money left");
        }
        else if (IsWordManagerEmpty() && UIManager.Instance != null && UIManager.Instance.IsSlotsEmpty())
        {
            Debug.Log("Game Over: No word stems left");
            TriggerGameOver(false, "Game Over: No word stems left");
        }
    }

    private bool IsFieldEmpty()
    {
        if (PlantManager.Instance != null)
            return PlantManager.Instance.IsEmpty();
        return true;
    }

    private bool IsWordManagerEmpty()
    {
        if (WordManager.Instance != null)
            return WordManager.Instance.IsEmpty();
        return true;
    }

    public void TriggerGameOver(bool won, string message)
    {
        isGameOver = true;
        isWin = won;
        DisableGameButtons();

        // Cancel any pending delayed instruction messages
        if (PlantManager.Instance != null)
            PlantManager.Instance.CancelInvoke();
            
        if (UIManager.Instance != null)
        {
            UIManager.Instance.CancelInvoke();
            UIManager.Instance.UpdateInstructionMessage(message);
            UIManager.Instance.UpdateGameOverMessage(message);
        }
    }

    public void DisableGameButtons()
    {
        UIManager.Instance.DisableSlots();
        PlantManager.Instance.DisableButtons();
    }

    public void EnableGameButtons()
    {
        UIManager.Instance.ActivateSlots();
        PlantManager.Instance.ActivateButtons();
    }
}
