using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private InputAction moveaction;
    private Animator animator;


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveaction = GetComponent<PlayerInput>().actions["Move"];
        moveaction.performed += HandleMove;
        moveaction.canceled += HandleMove;
        animator = GetComponent<Animator>();
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        Vector3 velocity = new Vector3(
            moveDirection.x * moveSpeed,
            rb.linearVelocity.y,
            moveDirection.z * moveSpeed

        );

        rb.linearVelocity = velocity;

        

    }
}