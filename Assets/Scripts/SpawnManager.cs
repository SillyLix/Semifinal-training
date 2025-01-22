using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnRange, spawnY, difficultyMultiplier = 1, maxSpawnSpeed = 5, minSpawnSpeed = 2, difficultyIncreaseTime = 15, enemySpawnAmount = 1, enemyTwoPropability = 0.1f;
    [SerializeField] private GameObject[] enemies;
    float currentTime;

    public GameObject rightWall;

    void Start()
    {
        StartCoroutine(EnemySpawn());

        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    IEnumerator EnemySpawn()
    {
        int maxSpawnAmount = 1;

        while (true)
        {
            if (currentTime > difficultyIncreaseTime)
            {
                currentTime = 0;
                difficultyMultiplier += 1;
                maxSpawnAmount++;
            }
            
            
            float waitTime = UnityEngine.Random.Range(minSpawnSpeed / (0.5f + 0.5f * difficultyMultiplier), maxSpawnSpeed / (0.5f + 0.5f * difficultyMultiplier));
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < maxSpawnAmount; i++)
            {
                OneEnemySpawn();
            }
        }


    }

    public void OneEnemySpawn()
    {
        float currentProbability = enemyTwoPropability * Mathf.Pow(1.1f, difficultyMultiplier);

        float randomValue = UnityEngine.Random.value;

        int enemy = 0;
        if (randomValue < currentProbability)
        {
            enemy = 1;
        }
        spawnRange = rightWall.transform.position.x - 6f;
        Instantiate(enemies[enemy], new Vector2(UnityEngine.Random.Range(-spawnRange, spawnRange), spawnY), enemies[enemy].transform.rotation);
    }
}