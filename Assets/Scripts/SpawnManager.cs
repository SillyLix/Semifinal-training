using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnY, difficultyMultiplier = 1, maxSpawnSpeed = 5, minSpawnSpeed = 2, difficultyIncreaseTime = 15, enemySpawnAmount = 1, enemyTwoPropability = 0.1f;
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
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        

        float spawnRange = rightWall.transform.position.x - 10f;
        Instantiate(enemy, new Vector2(UnityEngine.Random.Range(-spawnRange, spawnRange), spawnY), enemy.transform.rotation);
    }
}