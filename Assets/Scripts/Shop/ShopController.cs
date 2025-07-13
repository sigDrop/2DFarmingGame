using UnityEngine;

public class ShopController : MonoBehaviour
{
    public ShopSeedDisplay[] seeds;

    public ShopCropDisplay[] crops;

    public void OpenClose()
    {
        if (UIController.instance.inventoryController.gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gameObject.activeSelf)
            {
                foreach (ShopSeedDisplay seed in seeds)
                {
                    seed.UpdateDisplay();
                }

                foreach (ShopCropDisplay crop in crops)
                {
                    crop.UpdateDisplay();
                }
            }    
        }
    }
}
