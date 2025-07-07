using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;

    public InputActionReference moveInput;

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

        animator.SetFloat("speed", rigidbody.linearVelocity.magnitude);
    }
}