using System.Collections;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [Header("Timers")]
    public int fireInterval;
    public int laserCooldown;
    private int rerollTime = 5;

    // Prefabs
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform centerPoint;

    // Sound
    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(fireLaser(fireInterval, laserPrefab));
    }

    private IEnumerator fireLaser(int interval, GameObject laser)
    {
        while (true)
        {
            yield return new WaitForSeconds(rerollTime);

            // Generate Random Number For Fire Chance
            int spawnChance = Random.Range(0, 5);
            if (spawnChance == 0)
            {
                // Interval Pause
                yield return new WaitForSeconds(interval);

                // Spawn Laser Beam
                audioManager.PlayerSFX(audioManager.laser);
                GameObject newLaser = Instantiate(laser, centerPoint.position, centerPoint.rotation);

                yield return new WaitForSeconds(interval);
                Destroy(newLaser);

                // Enter Cooldown Phase For Laser
                yield return new WaitForSeconds(laserCooldown);
            }
        }
    }
}
