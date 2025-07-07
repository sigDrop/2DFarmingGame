using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;

    public InputActionReference moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;
    }
}