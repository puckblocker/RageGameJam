using System.Collections;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public int fireInterval;
    public int bulletCount = 3;
    [SerializeField] private float offsetVal = 5f;
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private float bulletSpd = 5f;

    // Prefabs
    [SerializeField] private GameObject laserPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(fireBullet(fireInterval, laserPrefab));
    }

    private IEnumerator fireBullet(int interval, GameObject bullet)
    {
        while (true)
        {
            // Interval Pause
            yield return new WaitForSeconds(interval);

            // Spawn Bullets
            for (int i = 0; i < bulletCount; i++)
            {
                Vector3 offset = transform.right * offsetVal;
                Vector3 spawnLoc = transform.position + offset;
                GameObject newBullet = Instantiate(bullet, spawnLoc, Quaternion.identity);
                bulletRb = newBullet.GetComponent<Rigidbody2D>();
                bulletRb.linearVelocity = transform.right * bulletSpd;

                yield return new WaitForSeconds(2f);
                Destroy(newBullet);
            }
        }
    }
}
