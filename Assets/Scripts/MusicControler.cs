using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControler : MonoBehaviour
{
    #region Singleton
    public static MusicControler instance;

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

    public bool isMusicOn;


    public void Toggle() 
    {
        if (isMusicOn)
        {
            isMusicOn = false;
        }
        else 
        {
            isMusicOn = true;
        }

        UpdateMusicStatus();
    }

    public void UpdateMusicStatus()
    {
        if (isMusicOn)
        {
            BGMManager.instance.isBGMOn = true;
            SFXManager.instance.isSFXOn = true;
        }
        else
        {
            BGMManager.instance.isBGMOn = false;
            SFXManager.instance.isSFXOn = false;
        }
    }
}
