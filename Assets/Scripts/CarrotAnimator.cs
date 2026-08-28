using UnityEngine;

public class CarrotAnimator : MonoBehaviour
{
    private PlantStatus plantStatus;
    private Animator animator;
    private static readonly int CarrotLevelHash = Animator.StringToHash("carrotLevel");

    // Start is called before the first frame update
    void Awake()
    {
        plantStatus = GetComponent<PlantStatus>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (plantStatus)
            plantStatus.OnWordGrown += UpdateAnimation;
    }
    private void OnDisable()
    {
        if (plantStatus)
            plantStatus.OnWordGrown -= UpdateAnimation;
    }

    private void UpdateAnimation(int currentLevel)
    {
        animator.SetInteger(CarrotLevelHash, currentLevel + 1);
        Debug.Log("Animator called " + currentLevel);
    }
    
}