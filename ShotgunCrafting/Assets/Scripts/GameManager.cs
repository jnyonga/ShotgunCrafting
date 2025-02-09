using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    int maxEnemies = 10;
    public float spawnInterval = 5f;

    private float spawnTimer = 0f;

    private void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount < maxEnemies)
        {
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if(spawnPoints.Length >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            }
            
        }
        else
        {

        }
    }


}
