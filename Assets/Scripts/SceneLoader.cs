using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /*
    public void LoadGamePlayScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SFXManager.instance.ManageSFX(3);
    }

    public void LoadStartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    */

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("StartMenuScene");
        //SFXManager.instance.ManageSFX(3);
    }

    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene("GamePlayScene");
        //SFXManager.instance.ManageSFX(3);
    }
}

