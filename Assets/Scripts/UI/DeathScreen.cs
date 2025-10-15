using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    public void RetryLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void GoodEnding()
    {
        SceneManager.LoadScene("CutsceneEndGood");
    }

    public void BadEnding()
    {
        SceneManager.LoadScene("CutsceneEndBad");
    }
}
