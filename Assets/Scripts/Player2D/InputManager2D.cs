using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager2D : MonoBehaviour
{
    // VARIABLES
    // Input System
    private PlayerControls controls;
    private PlayerControls.Player2DActions playerAct;
    // Scripts
    private PlayerMovement2D movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Initialize Components
        movement = GetComponent<PlayerMovement2D>();

        // Initialize Inputs
        controls = new PlayerControls();
        playerAct = controls.Player2D;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.move(playerAct.Move.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
