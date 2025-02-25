using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();

    [SerializeField] private GameObject player;

    private void Start() 
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody playerRB = player?.GetComponent<Rigidbody>();

        if(Input.GetKeyDown(KeyCode.Space) && playerRB.transform.position.y < 1.1f)
        {
            OnSpacePressed?.Invoke();
        }
        
        // Add left and right positioning for the ball
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            input += Vector2.right;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            input += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            input += Vector2.down;
        }
        OnMove?.Invoke(input);

    }
}