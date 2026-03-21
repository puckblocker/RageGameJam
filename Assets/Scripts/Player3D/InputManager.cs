using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    // VARIABLES
    // Input System
    private PlayerControls controls;
    private PlayerControls.Player3DActions playerAct;
    // Scripts
    private PlayerMovement movement;
    [SerializeField] private VideoScroller videoScroller;
    [SerializeField] private ImageMover imageMover;
    private PhoneBase phoneBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Initialize Components
        movement = GetComponent<PlayerMovement>();

        // Initialize Inputs
        controls = new PlayerControls();  
        playerAct = controls.Player3D;
        playerAct.Throw.performed += ctx => phoneBase.ThrowPhone();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.move(playerAct.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        movement.lookDir(playerAct.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable(); 
    }

    void Update()
    {
        if(playerAct.SwipeDown.WasPressedThisFrame())
        {
            //videoScroller.switchVideo();
            imageMover.SwipeDown();
        }

        if (playerAct.SwipeUp.WasPressedThisFrame())
        {
            //videoScroller.switchVideo();
            imageMover.SwipeUp();
        }
    }
}
