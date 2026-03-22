using System.Collections;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public int fireInterval;
    [SerializeField] private float offsetVal = 5f;

    // Prefabs
    [SerializeField] private GameObject laserPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(fireLaser(fireInterval, laserPrefab));
    }

    private IEnumerator fireLaser(int interval, GameObject laser)
    {
        while (true)
        {
            // Interval Pause
            yield return new WaitForSeconds(interval);

            // Spawn Laser Beam
            Vector3 offset = transform.right * offsetVal;
            Vector3 spawnLoc = transform.position + offset;
            GameObject newLaser = Instantiate(laser, spawnLoc, Quaternion.identity);

            yield return new WaitForSeconds(interval);
            Destroy(newLaser);
        }
    }
}
