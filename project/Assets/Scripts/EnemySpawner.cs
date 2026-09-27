using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // public GameObject enemyPrefab;

    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 0.5f;

    public float fallSpeed = 5f;
    public float xLimit;

    public float spawnY = 8f;
    private float spawnNext = 0f;

    // Update is called once per frame
    void Update()
    {
        spawnRatePerMinute += spawnRateIncrement * Time.deltaTime;

        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + (60f / spawnRatePerMinute);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float randX = Random.Range(-xLimit, xLimit);
        Vector2 spawnPosition = new Vector2(randX, spawnY);

        // GameObject enemy = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        GameObject enemy = Pooling.Instance.GetEnemy();
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;

        Rigidbody _rigidbody = enemy.GetComponent<Rigidbody>();
        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = new Vector2(0f, -fallSpeed);
            _rigidbody.angularVelocity = Vector2.zero;
        }

        // Destroy(enemy, maxTimeLife);
    }
}
