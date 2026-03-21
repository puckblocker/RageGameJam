using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;



// Automatically Grab Components
[RequireComponent(typeof(Rigidbody), typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    // Variables
    private float spd = 5.0f;

    // Component Variables
    private Rigidbody rb;
    private PlayerBase player;

    [Header("Sensitivity")]
    [SerializeField] private float vertSens = 10.0f;
    [SerializeField] private float horizSens = 10.0f;

    // Camera & Player Transforms
    [Header("Transforms")]
    [SerializeField] private float xRot = 0;    // ensures player rotates with camera
    [SerializeField] private Camera cam;
    [SerializeField] Transform playerBdy;

    private void Awake()
    {
        // Grab Components
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerBase>();

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void move(Vector2 moveInput)
    {
        // Player Movement
        Vector3 playerVelocity = new Vector3(moveInput.x * spd, rb.linearVelocity.y, moveInput.y * spd);    // find player's velocity
        rb.linearVelocity = transform.TransformDirection(playerVelocity);   // set player's velocity
    }

    public void lookDir(Vector2 mouseInput)
    {
        // Grab Mouse Inputs
        Vector2 mouse;
        mouse.x = mouseInput.x * horizSens * Time.deltaTime; // delta time to make it framerate independent
        mouse.y = mouseInput.y * vertSens * Time.deltaTime;

        // Vertical Movement
        xRot -= mouse.y;    // 
        xRot = Mathf.Clamp(xRot, -100f, 55f);    // locks player view distance rotation
        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f); // quaternion euler allows for rotation and rotation only for left and right

        // Rotate Player With Camera
        playerBdy.Rotate(Vector3.up * mouse.x, Space.World);
    }

    //public void processInteract()
    //{
    //    Debug.Log("Interact");
    //    videoScroller.switchVideo();
    //}
}
