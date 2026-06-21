using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System library

public class ToddlerPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        // 1. Read input from Mobile Virtual Stick or Keyboard standard controller
        if (Gamepad.all.Count > 0)
        {
            inputVector = Gamepad.current.leftStick.ReadValue();
        }

        // Fallback to keyboard inputs if mobile joystick isn't actively being pressed
        if (inputVector.magnitude < 0.1f)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            inputVector = new Vector2(moveX, moveZ);
        }

        // 2. Convert to World Space 3D directional vector
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        // 3. Move and Rotate using Rigidbody
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 targetPosition = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}