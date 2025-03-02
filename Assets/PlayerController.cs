using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float airControlFactor = 0.5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
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
        bool grounded = controller.isGrounded;
        
        if (grounded)
        {
            playerVelocity.y = -2f;
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

        float currentTurnSpeed = grounded ? turnSpeed : turnSpeed * 0.5f;
        if (moveDirection.magnitude > 0.1f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentTurnSpeed);
        }

        float currentSpeed = grounded ? speed : speed * airControlFactor;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

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

}
