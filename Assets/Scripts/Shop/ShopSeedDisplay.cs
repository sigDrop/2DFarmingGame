using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSeedDisplay : MonoBehaviour
{
    public CropController.CropType cropType;

    public Image seedImage;
    public TMP_Text seedAmount, priceText;

    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(cropType);

        seedImage.sprite = info.seedType;
        seedAmount.text = "x" + info.seedAmount;

        priceText.text = "$" + info.seedPrice + " each";
    }

    public void BuySeed(int amount)
    {
        CropInfo info = CropController.instance.GetCropInfo(cropType);

        if (CurrencyController.instance.CheckMoney(info.seedPrice * amount))
        {
            CropController.instance.AddSeed(cropType, amount);

            CurrencyController.instance.SpendMoney(info.seedPrice * amount);

            UpdateDisplay();
        }
    }
}
