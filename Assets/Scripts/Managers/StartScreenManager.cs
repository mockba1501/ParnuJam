using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (BGMManager.Instance != null)
            BGMManager.Instance.ManageBGM(0);
    }
}