using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolBarActivatorIcons;

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
}
