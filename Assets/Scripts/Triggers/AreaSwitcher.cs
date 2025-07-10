using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSwitcher : MonoBehaviour
{
    public string sceneToLoad;

    public Transform startPoint;

    public string transitionName;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Transition"))
        {
            if (PlayerPrefs.GetString("Transition") == transitionName)
            {
                PlayerController.instance.transform.position = startPoint.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);

            PlayerPrefs.SetString("Transition", transitionName);
        }
    }
}
