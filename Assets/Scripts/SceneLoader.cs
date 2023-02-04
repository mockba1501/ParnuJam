using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SFXManager.instance.ManageSFX(2);
    }

    public void QuitGame() 
    {
        Application.Quit();
        BGMManager.instance.ManageBGM("Stop", 0);

        Debug.Log("Quit game");
    }
}
