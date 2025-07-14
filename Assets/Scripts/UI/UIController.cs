using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolBarActivatorIcons;

    public TMP_Text timeText;

    public InventoryController inventoryController;

    public Image seedImage;

    public ShopController shopController;

    public TMP_Text moneyText;

    public GameObject pauseScreen;

    public string sceneName;

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

    private void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            inventoryController.OpenClose();
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            shopController.OpenClose();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            PauseUnpause();
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

    public void SwitchSeed(CropController.CropType crop)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(crop).seedType;

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void UpdateMoney(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);

        Destroy(gameObject);
        Destroy(PlayerController.instance);
        Destroy(GridInfo.instance);
        Destroy(TimeController.instance);
        Destroy(CropController.instance);
        Destroy(CurrencyController.instance);

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
