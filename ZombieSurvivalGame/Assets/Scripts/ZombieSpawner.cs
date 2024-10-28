using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 0f, spawnInterval);
    }

    void SpawnZombie()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject zombiePrefab = zombiePrefabs[zombieIndex];

        Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
    }
}