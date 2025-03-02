using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool grounded;
    private int jumpsUsed = 0;
    public Transform cameraBody;
    private float gravity = -9.8f;

    private void Start()
    {
        // Get reference to CharacterController
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        grounded = controller.isGrounded;
        
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpsUsed = 0;
        }
        // Convert input direction based on camera
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = cameraBody.forward;
        Vector3 right = cameraBody.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        if (moveDirection.magnitude > 0.1f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        controller.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsUsed < 2)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpsUsed++;
        }
        

        if (!grounded)
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    [SerializeField] private float groundCheckDistance = 0.2f;
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

}
