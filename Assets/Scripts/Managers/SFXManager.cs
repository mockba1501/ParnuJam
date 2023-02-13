using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    #region Singleton
    public static SFXManager instance;

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
    public AudioClip[] sfxMusics;

    public void ManageSFX(int num) // num is index of sfxMusics: 0=, 1= , 2=
    {
        audioSource.PlayOneShot(this.sfxMusics[num]);
    }
}
