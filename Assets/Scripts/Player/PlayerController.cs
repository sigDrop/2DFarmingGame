using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator animator;

    public enum ToolType
    {
        plough,
        wateringCan,
        seeds,
        basket
    }
    public ToolType currentTool;

    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
    }

    void Update()
    {
        rigidbody.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

        if (rigidbody.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rigidbody.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        bool hasSwitchedTool = false;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;

            if((int) currentTool >=4)
            {
                currentTool = ToolType.plough;
            }

            hasSwitchedTool = true;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.plough;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;
            hasSwitchedTool = true;
        }

        if (hasSwitchedTool)
        {
            //FindFirstObjectByType<UIController>().SwitchTool((int)currentTool);
            UIController.instance.SwitchTool((int)currentTool);
        }

        if (actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }

        animator.SetFloat("speed", rigidbody.linearVelocity.magnitude);
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        if (block != null)
        {
            switch (currentTool) 
            {
                case ToolType.plough:
                    block.PloughSoil();
                    break;
                
                case ToolType.wateringCan:

                    break;

                case ToolType.seeds:

                    break;

                case ToolType.basket:

                    break;
            }
        }
    }
}