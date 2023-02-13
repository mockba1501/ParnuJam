using UnityEngine;
using System.Collections;
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
    public bool isBGMOn;
    //public bool isPlayingBgm;

    /*
    public void ManageBGM(int num) // num is index of backGroundMusics: 0=BgmStart, 1=BgmPlay, 2=BgmEnd
    {
        audioSource.clip = this.backGroundMusics[num];

        if (isBGMOn) 
        {
            if (!isPlayingBgm)
            {
                audioSource.Play();
                isPlayingBgm = true;
            }
        }
    }
    */

    public void ManageBGM(int num) // num is index of backGroundMusics: 0=BgmStart, 1=BgmPlay, 2=BgmEnd
    {
        audioSource.clip = this.backGroundMusics[num];

        if (isBGMOn)
        {

            audioSource.Play();
            //isPlayingBgm = true;


            /*if (!isPlayingBgm) 
            {
                audioSource.Play();
                isPlayingBgm = true;
            }*/
        }
        else 
        {

            audioSource.mute = !audioSource.mute;

            /*
            if (isPlayingBgm) 
            {
                audioSource.Stop();
                isPlayingBgm = false;
            }*/
        }
    }
}
