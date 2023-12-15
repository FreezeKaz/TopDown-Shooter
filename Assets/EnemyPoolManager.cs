using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    private static EnemyPoolManager _instance;
    public static EnemyPoolManager Instance => _instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> objectPool;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
        InitPool();
    }

    private void InitPool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            objectPool.Add(enemy);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        GameObject newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.SetActive(false);
        objectPool.Add(newEnemy);

        return newEnemy;
    }
    
}
