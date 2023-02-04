using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{

    #region Singleton
    public static BGMManager instance;

    void Awake()
    {
        if (instance != null)
        {
            if (SceneManager.GetActiveScene().name == "StartScreen")
            {
                Destroy(gameObject);
            }
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);// not delete data

    }
    #endregion



    public AudioSource audioSource;
    public AudioClip[] backGroundMusics;
    public bool isPlayingBgm;

    public void ManageBGM(string action, int num) // num is index of backGroundMusics: 0=BgmStart, 1=BgmPlay, 2=BgmEnd
    {
        audioSource.clip = this.backGroundMusics[num];

        if (!isPlayingBgm && action == "Play")
        {
            audioSource.Play();
            isPlayingBgm = true;
        }
        else if (isPlayingBgm && action == "Stop")
        {
            audioSource.Stop();
            isPlayingBgm = false;
        }
    }
}
