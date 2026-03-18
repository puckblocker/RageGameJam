using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Automatically Grab Components
[RequireComponent(typeof(Rigidbody2D), typeof(PlayerBase))]
public class PlayerMovement2D : MonoBehaviour
{
    // Variables
    InputAction moveAction;
    private float spd = 5.0f;
    // Component Variables
    private Rigidbody2D rb;
    private PlayerBase player;

    private void Awake()
    {
        // Grab Components
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBase>();
    }

    public void move(Vector2 moveInput)
    {
        // Player Movement
        rb.linearVelocity = new Vector2(moveInput.x * spd, moveInput.y * spd);
    }
}
