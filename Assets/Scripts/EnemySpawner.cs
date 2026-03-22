using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Colliders
    public BoxCollider2D[] spawnZones;

    // Prefabs
    [SerializeField] private GameObject laserEnemyPrefab;
    [SerializeField] private float laserInterval = 3.5f;

    [SerializeField] private GameObject gunEnemyPrefab;
    [SerializeField] private float gunInterval = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemy(laserInterval, laserEnemyPrefab));
        StartCoroutine(spawnEnemy(gunInterval, gunEnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        // Interval Pause
        yield return new WaitForSeconds(interval);

        // Top Zone
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(spawnZones[0].bounds.min.x, spawnZones[0].bounds.max.x), Random.Range(spawnZones[0].bounds.min.y, spawnZones[0].bounds.max.y), 0f), Quaternion.identity);


        // Spawn Enemies At Random Positon
        //GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, -5), Random.Range(-6f, -6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
