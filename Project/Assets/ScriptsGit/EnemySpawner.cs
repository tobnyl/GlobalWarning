using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public float baseTime = 2f;
    public float spawnTimer;
    public GameObject enemyPrefab;
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int corX;
        var random = Random.Range(0, 2);
        if (random == 0)
        {
            SetupEnemy(1);
        }
        else
        {
            SetupEnemy(-1);
        }

    }

    void SetupEnemy(int i)
    {

        GameObject enemy = Instantiate(enemyPrefab, new Vector2(i * 25, Random.Range(-10, 11)), Quaternion.identity) as GameObject;
        enemy.SetActive(true); //Only used because prefab is inactive at the moment
        enemy.GetComponent<EnemyMovement>().Initialize(i);
        baseTime *= 0.98f;

        if (baseTime > 0.5f)
        {
            spawnTimer = baseTime;
        }
        else spawnTimer = 0.5f;
    }
}
