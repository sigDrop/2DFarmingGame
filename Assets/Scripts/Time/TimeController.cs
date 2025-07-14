using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public float currentTime;

    public float dayStart, dayEnd;

    public float timeSpeed = .25f;

    private bool _timeActive;

    public int currentDay = 1;

    public string dayEndScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = dayStart;
        _timeActive = true;
    }

    void Update()
    {
        if (_timeActive)
        {
            currentTime += Time.deltaTime * timeSpeed;

            if (currentTime > dayEnd)
            {
                currentTime = dayEnd;

                EndDay();
            }

            if (UIController.instance != null)
            {
                UIController.instance.UpdateTimeText(currentTime);
            }
        }
    }

    public void StartDay()
    {
        _timeActive = true;

        currentTime = dayStart;

        AudioManager.instance.PlaySFX(6);
    }    

    public void EndDay()
    {
        _timeActive = false;
        currentDay++;

        GridInfo.instance.GrowCrop();

        PlayerPrefs.SetString("Transitrion", "WakeUp");

        SceneManager.LoadScene(dayEndScene);
    }
}
