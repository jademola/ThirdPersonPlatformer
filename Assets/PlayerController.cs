using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float drag1;
    private Rigidbody playerRB;
    bool grounded;
    public LayerMask isGround;
    public float playerHeight;

    private void Start()
    {
        // Get reference to player rigidbody
        playerRB = GetComponent<Rigidbody>();

        //Listen for horizontal movement
        inputManager.OnMove.AddListener(MovePlayer);

        //Listen for space bar to allow player to jump
        inputManager.OnSpacePressed.AddListener(JumpPlayer);


    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f +0.2f, isGround);

        if (grounded)
        {
            playerRB.linearDamping = drag1;
        } else
        {
            playerRB.linearDamping = 0;
        }
    }

    private void MovePlayer(Vector2 direction) {
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        playerRB.AddForce(speed * moveDirection);
    }

    private void JumpPlayer() {
        Vector3 jumpVector = new Vector3(0, 10f, 0);
        playerRB.AddForce(jumpVector, ForceMode.Impulse);
    }
}
