using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator animator;

    void Start()
    {
       
    }

    void Update()
    {
        rigidbody.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

        if(rigidbody.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rigidbody.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        if(actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }

        animator.SetFloat("speed", rigidbody.linearVelocity.magnitude);
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        block.PloughSoil();
    }
}