using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float moneyToWin = 15000f;

    public GameObject victoryScreen;

    private void Start()
    {
        victoryScreen.SetActive(false);
    }

    private void Update()
    {
        if (CurrencyController.instance.currentMoney >= moneyToWin)
        {
            victoryScreen.SetActive(true);
        }
    }
}
