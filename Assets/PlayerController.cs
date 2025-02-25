using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float force = 1f;
    [SerializeField] private float speed = 2f;
    private Rigidbody playerRB;

    private void Start()
    {
        // Get reference to player rigidbody
        playerRB = GetComponent<Rigidbody>();

        //Listen for horizontal movement
        inputManager.OnMove.AddListener(MovePlayer);

        //Listen for space bar to allow player to jump
        inputManager.OnSpacePressed.AddListener(JumpPlayer);


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
