using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

// Automatically Grab Components
[RequireComponent(typeof(Rigidbody2D), typeof(PlayerBase))]
public class PlayerMovement2D : MonoBehaviour
{
    // Variables
    InputAction moveAction;
    private bool canDmg = true;
    [SerializeField] private float invFrame = 0.5f;

    // Component Variables
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerBase player;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private StatsUI2D UI;
    [SerializeField] private GameObject gameOver;

    // Sound
    AudioManager audioManager;

    private void Awake()
    {
        // Grab Components
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBase>();

        gameOver.SetActive(false);
    }

    public void move(Vector2 moveInput)
    {
        // Player Movement
        rb.linearVelocity = new Vector2(moveInput.x * player.speed, moveInput.y * player.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check For Collision With Bullet
        if (collision.gameObject.CompareTag("bullet") && canDmg == true)
        {
            // Destroy Colliding Object And Lower Health
            audioManager.PlayerSFX(audioManager.hit);
            Destroy(collision.gameObject);
            PlayerBase.healthVal--;
            UI.healthBar();

            // Queue Death When No Life
            if (PlayerBase.healthVal <= 0)
                onDeath();
            // Queue Invincible Frames
            else
                StartCoroutine(Invincible(invFrame));

        }

        // Check For Collision With Laser
        else if (collision.gameObject.CompareTag("laser") && canDmg == true)
        {
            // Lower Health
            PlayerBase.healthVal--;
            UI.healthBar();

            // Queue Death When No Life
            if (PlayerBase.healthVal <= 0)
                onDeath();
            // Queue Invincible Frames
            else
                StartCoroutine(Invincible(invFrame));

        }
    }

    public void onDeath()
    {
        audioManager.PlayerSFX(audioManager.death);
        audioManager.StopMusic();
        audioManager.PlayerSFX(audioManager.gameOver);
        player.rageCnt++;
        gameOver.SetActive(true);
        Destroy(playerObj);
    }

    private IEnumerator Invincible(float invFrame)
    {
        yield return new WaitForSeconds(invFrame);
    }
}
