using System.Collections;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [Header("Timers")]
    public int fireInterval;
    public int bulletCooldown;
    private int rerollTime = 3;

    public int bulletCount = 3;
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private float burstSpd = 5f;

    // Prefabs
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform centerPoint;

    // Sound
    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(fireBullet(fireInterval, bulletPrefab));
    }

    private IEnumerator fireBullet(int interval, GameObject bullet)
    {
        while (true)
        {
            // Time To Reroll
            yield return new WaitForSeconds(rerollTime);

            // Generate Random Number For Spawn Chance
            int spawnChance = Random.Range(0, 5);

            if (spawnChance == 0 || spawnChance == 1)
            {
                // Interval Pause
                yield return new WaitForSeconds(interval);

                // Spawn Bullets
                for (int i = 0; i < bulletCount; i++)
                {
                    // Spawn Bullet Object
                    audioManager.PlayerSFX(audioManager.bullet);
                    GameObject newBullet = Instantiate(bullet, centerPoint.position, centerPoint.rotation);

                    // Launch Bullet
                    bulletRb = newBullet.GetComponent<Rigidbody2D>();
                    bulletRb.linearVelocity = transform.right * burstSpd;

                    // Delete Bullet
                    Destroy(newBullet, 2f);

                    yield return new WaitForSeconds(burstSpd);
                }

                // Enter Cooldown Phase For Bullet
                yield return new WaitForSeconds(bulletCooldown);
            }
        }
    }
}
