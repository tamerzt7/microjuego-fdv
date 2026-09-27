using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public GameObject fragmentPrefab;

    public int bulletPoolSize = 30;
    public int enemyPoolSize = 10;
    public int fragmentPoolSize = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private Queue<GameObject> fragmentPool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        InitializePool(bulletPool, bulletPrefab, bulletPoolSize);
        InitializePool(enemyPool, enemyPrefab, enemyPoolSize);
        InitializePool(fragmentPool, fragmentPrefab, fragmentPoolSize);
    }

    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetBullet() => GetFromPool(bulletPool, bulletPrefab);
    public GameObject GetEnemy() => GetFromPool(enemyPool, enemyPrefab);
    public GameObject GetFragment() => GetFromPool(fragmentPool, fragmentPrefab);

    private GameObject GetFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            GameObject go = pool.Dequeue();
            go.SetActive(true);
            return go;
        }

        return Instantiate(prefab);
    }

    public void ReturnBullet(GameObject go)
    {
        go.SetActive(false);
        bulletPool.Enqueue(go);
    }

    public void ReturnEnemy(GameObject go)
    {
        go.SetActive(false);
        enemyPool.Enqueue(go);
    }

    public void ReturnFragment(GameObject go)
    {
        go.SetActive(false);
        fragmentPool.Enqueue(go);
    }
}