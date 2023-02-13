using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicControler.instance.isMusicOn = true;
        MusicControler.instance.UpdateMusicStatus();
        BGMManager.instance.ManageBGM(0);
    }
}