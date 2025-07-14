using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToStart;

    private void Start()
    {
        AudioManager.instance.PlayMusic();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart);

        AudioManager.instance.PlayNextMusic();

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }    

    public void QuitGame()
    {
        Application.Quit();

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }
}
