using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // VARIABLES
    // Input System
    private PlayerControls controls;
    private PlayerControls.Player3DActions playerAct;
    // Scripts
    private PlayerMovement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Initialize Components
        movement = GetComponent<PlayerMovement>();

        // Initialize Inputs
        controls = new PlayerControls();  
        playerAct = controls.Player3D;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.move(playerAct.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
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
