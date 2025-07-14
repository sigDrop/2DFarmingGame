using UnityEngine;
using UnityEngine.InputSystem;

public class ShopActivator : MonoBehaviour
{
    private bool _IsCanOpen;

    private void Update()
    {
        if (_IsCanOpen)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (!UIController.instance.shopController.gameObject.activeSelf)
                {
                    UIController.instance.shopController.OpenClose();

                    AudioManager.instance.PlaySFX(0);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _IsCanOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _IsCanOpen = false;
        }
    }
}
