using TMPro;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolBarActivatorIcons;

    public TMP_Text timeText;

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

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolBarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolBarActivatorIcons[selected].SetActive(true);
    }

    public void UpdateTimeText(float currentTime)
    {
        if (currentTime < 12)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + "AM";
        }
        else if (currentTime < 13)
        {
            timeText.text = "12PM";
        }
        else if (currentTime < 24)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12) + "PM";
        }
        else if (currentTime < 25)
        {
            timeText.text = "12 AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24) + "AM";
        }
    }
}
