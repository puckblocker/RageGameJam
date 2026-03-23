using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;



// Automatically Grab Components
[RequireComponent(typeof(Rigidbody), typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameOver gameOver;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundSource;

    [Header("SFX")]
    public AudioClip slam;

    // Variables
    private float spd = 5.0f;

    // Component Variables
    private Rigidbody rb;
    private PlayerBase player;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject phone;

    [SerializeField] private Animator sceneAnim;

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

        // Coroutine For Rage State
        StartCoroutine(rageMeter());
    }

    public void slamSFX()
    {
        soundSource.clip = slam;
        soundSource.PlayOneShot(slam);
    }

    private IEnumerator rageMeter()
    {
        switch (PlayerBase.rageCnt)
        {
            case 0:
                // Grace Period
                phone.SetActive(false);
                yield return new WaitForSeconds(10);

                // Unlock Cursor
                Cursor.lockState = CursorLockMode.None;

                sceneAnim.Play("Crossfade_End");
                yield return new WaitForSeconds(2);

                // Switch To 2D
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case 1:
                phone.SetActive(true);
                yield return new WaitForSeconds(2);
                // Play Fist Slam Animation
                animator.Play("FistSlam", 3);
                // Play Doom Scrolling Animation
                yield return new WaitForSeconds(3);
                animator.Play("texting", 3);
                yield return new WaitForSeconds(10);

                // Unlock Cursor
                Cursor.lockState = CursorLockMode.None;

                sceneAnim.Play("Crossfade_End");
                yield return new WaitForSeconds(2);

                // Switch To 2D
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case 2:
                // Grace Period
                phone.SetActive(true);

                // Play Fist Slam Animation
                yield return new WaitForSeconds(2);
                animator.Play("FistSlam", 3);

                // Play Doom Scrolling Animation
                yield return new WaitForSeconds(3);
                animator.Play("texting", 3);

                // Play Throw Phone Animation
                yield return new WaitForSeconds(5);
                animator.Play("ThrowObject", 3);
                // Switch To 2D
                yield return new WaitForSeconds(3);
                phone.SetActive(false);

                // Unlock Cursor
                Cursor.lockState = CursorLockMode.None;

                sceneAnim.Play("Crossfade_End");
                yield return new WaitForSeconds(2);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case 3:
                // Grace Period
                phone.SetActive(false);
                yield return new WaitForSeconds(5);
                // Play Fist Slam Animation
                animator.Play("FistSlam", 3);
                // Play Punch Animation
                yield return new WaitForSeconds(3);
                animator.Play("punch", 3);
                yield return new WaitForSeconds(1);
                // Trigger game Over Screen
                sceneAnim.Play("Crossfade_End");
                yield return new WaitForSeconds(2);
                gameOver.gameEnd();
                break;

            default:
                break;
        };
    }

    //public void move(Vector2 moveInput)
    //{
    //    // Player Movement
    //    Vector3 playerVelocity = new Vector3(moveInput.x * spd, rb.linearVelocity.y, moveInput.y * spd);    // find player's velocity
    //    rb.linearVelocity = transform.TransformDirection(playerVelocity);   // set player's velocity
    //}

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
