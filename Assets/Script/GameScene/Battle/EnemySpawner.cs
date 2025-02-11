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
    public float spawnInterval = 2f;
    public int enemySpawnCount = 0;

    public IEnumerator EnemySpawn()
    {
        while (true)
        {
            int maxEnemyIndex = Mathf.Min(enemySpawnCount / 8, enemys.Length - 1);
            GameObject enemy = Instantiate(GetRandomEnemy(maxEnemyIndex), transform.position, Quaternion.identity);
            spawnInterval = Mathf.Max(2f, spawnInterval - 0.1f);
            enemySpawnCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomEnemy(int maxEnemyIndex)
    {
        float totalChance = 0f;
        foreach (var enemy in enemys)
        {
            totalChance += enemy.spawnChance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulativeChance = 0f;

        for (int i = 0; i <= maxEnemyIndex; i++)
        {
            cumulativeChance += enemys[i].spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return enemys[i].enemyPrefab;
            }
        }

        return enemys[0].enemyPrefab;
    }
}