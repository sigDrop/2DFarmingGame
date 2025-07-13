using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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

    public Transform toolIndicator;
    public float toolRange = 3f;

    public CropController.CropType seedCropType;

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

    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);

        UIController.instance.SwitchSeed(seedCropType);
    }

    void Update()
    {
        if (UIController.instance != null)
        {
            if (UIController.instance.inventoryController != null)
            {
                if (UIController.instance.inventoryController.gameObject.activeSelf == true)
                {
                    rigidbody.linearVelocity = Vector2.zero;
                    return;
                }
            }

            if (UIController.instance.shopController != null)
            {
                rigidbody.linearVelocity = Vector2.zero;
                return;
            }
        }

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

        animator.SetFloat("speed", rigidbody.linearVelocity.magnitude);

        if (GridController.instance != null)
        {
            if (actionInput.action.WasPressedThisFrame())
            {
                UseTool();
            }

            toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

            if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
            {
                Vector2 direction = toolIndicator.position - transform.position;
                direction = direction.normalized * toolRange;
                toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
            }

            toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + .5f,
                Mathf.FloorToInt(toolIndicator.position.y) + .5f, 0f);
        }
        else
        {
            toolIndicator.position = new Vector3(0f, 0f, -20f);
        }
    }

    void UseTool()
    {
        GrowBlock block = null;

        //block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        block = GridController.instance.GetBlock(toolIndicator.position.x - .5f, toolIndicator.position.y - .5f);

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
                    if (CropController.instance.GetCropInfo(seedCropType).seedAmount > 0)
                    {
                        block.PlantCrop(seedCropType);

                        //CropController.instance.UseSeed(seedCropType);
                    }
                    break;

                case ToolType.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }

    public void SwitchSeed(CropController.CropType newSeed)
    {
        seedCropType = newSeed;
    }
}