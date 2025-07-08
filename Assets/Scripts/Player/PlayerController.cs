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

    public float toolWaitTime = 0.5f;

    private float _toolWaitCounter;

    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
    }

    void Update()
    {
        if (_toolWaitCounter > 0)
        {
            _toolWaitCounter -= Time.deltaTime;
            rigidbody.linearVelocity = Vector3.zero;
        }
        else
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

        _toolWaitCounter = toolWaitTime;

        if (block != null)
        {
            switch (currentTool) 
            {
                case ToolType.plough:
                    block.PloughSoil();
                    animator.SetTrigger("usePloughing");
                    break;
                
                case ToolType.wateringCan:
                    block.WaterSoil();
                    animator.SetTrigger("useWateringCan");
                    break;

                case ToolType.seeds:
                    block.PlantCrop();
                    break;

                case ToolType.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }
}