using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToStart;

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }    
}
