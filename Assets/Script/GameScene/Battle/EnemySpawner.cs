using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyObject
{
    public GameObject enemyPrefab;
    public float spawnChance;
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyObject[] enemys;

    public float spawnInterval;
    public int enemySpawnCount;

    public IEnumerator EnemySpawn()
    {
        while (true)
        {
            GameObject enemy = Instantiate(enemys[0].enemyPrefab, transform.position, Quaternion.identity);
            spawnInterval = Mathf.Max(2f, spawnInterval - 0.05f);
            enemySpawnCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
