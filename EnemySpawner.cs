using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public bool spawnEnabled = false;
    [SerializeField]
    int maxEnemies = 30;
    [SerializeField]
    float minPositionX = -30;
    [SerializeField]
    float maxPositionX = 30;
    [SerializeField]
    float minPositionZ = -30;
    [SerializeField]
    float maxPositionZ = 10;
    [SerializeField]
    float minSpawnInterval = 0.1f;
    [SerializeField]
    float maxSpawnInterval = 0.5f;
    [SerializeField]
    GameObject[] enemyPrefabs;
    bool spawning = false;
    void Update()
    {
        if (spawnEnabled)
        {
            StartCoroutine(SpawnTimer());
        }
    }
    IEnumerator SpawnTimer()
    {
        if (!spawning)
        {
            if (SpawnEnemy())
            {
                spawning = true;
                float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(interval);
                spawning = false;
            }
            else
            {
                yield return null;
            }
        }
        yield return null;
    }
    bool SpawnEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length >= maxEnemies)
        {
            return false;
        }
        else
        {
            int choosedIndex = Random.Range(0, enemyPrefabs.Length);
            float diffPositionX = Random.Range(minPositionX, maxPositionX);
            float diffPositionZ = Random.Range(minPositionZ, maxPositionZ);
            Vector3 position = new Vector3(0f + diffPositionX, 0f, 0f + diffPositionZ);
            Instantiate(enemyPrefabs[choosedIndex], position, Quaternion.identity);
            return true;
        }
    }
}
