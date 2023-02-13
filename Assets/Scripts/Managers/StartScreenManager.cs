using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BGMManager.instance.ManageBGM(0);
    }
}