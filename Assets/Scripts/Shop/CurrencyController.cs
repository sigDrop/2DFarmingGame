using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;

    public float currentMoney;

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

    private void Start()
    {
        UIController.instance.UpdateMoney(currentMoney);
    }

    public void SpendMoney(float amountToSpend)
    {
        currentMoney -=amountToSpend;
        UIController.instance.UpdateMoney(currentMoney);
    }

    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
        UIController.instance.UpdateMoney(currentMoney);
    }

    public bool CheckMoney(float amount)
    {
        if (currentMoney >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
