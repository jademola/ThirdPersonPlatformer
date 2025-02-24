using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float force = 1f;
    [SerializeField] private float speed;
    private Rigidbody playerRB;

    private void Start()
    {
        // Get reference to player rigidbody
        playerRB = GetComponent<Rigidbody>();

        //Listen for horizontal movement
        inputManager.OnMove.AddListener(MovePlayer);


    }

    private void MovePlayer(Vector2 direction) {
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        playerRB.AddForce(speed * moveDirection);
    }
}
