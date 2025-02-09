using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    int maxEnemies = 12;
    public float spawnInterval = 5f;

    private float spawnTimer = 0f;

    private void Update()
    {
        if(playerHealth.health <= 0)
        {
            SceneManager.LoadScene(0);
        }

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
