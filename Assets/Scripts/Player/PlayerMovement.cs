using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Automatically Grab Components
[RequireComponent(typeof(Rigidbody), typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    // Variables
    InputAction moveAction;
    private float spd = 5.0f;
    private float velocity;
    // Component Variables
    private Rigidbody rb;
    private PlayerBase player;

    [Header("Sensitivity")]
    [SerializeField] private float xRot = 0;
    [SerializeField] private float vertSens = 10.0f;
    [SerializeField] private float horizSens = 10.0f;

    // Camera
    [SerializeField] private Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerBase>();
    }

    public void move(Vector2 input)
    {
        // Conversion input from 2D to 3D
        Vector3 moveDir = new Vector3(input.x, 0.0f, input.y);

        // Move Player
        Vector3 movement = moveDir * spd;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    public void lookDir(Vector2 input)
    {
        // Grab Mouse Inputs
        Vector2 mouse = new Vector2(input.x, input.y);

        // Vertical Movement
        xRot -= mouse.y * vertSens * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        // Horizontal Movement
        transform.Rotate(Vector3.up * (mouse.x * Time.deltaTime) * horizSens);
    }
}
