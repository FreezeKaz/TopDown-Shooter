using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    private static EnemyPoolManager _instance;
    public static EnemyPoolManager Instance => _instance;

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int poolSize;
    }

    public class GameObjectPools
    {
        public string name;
        public List<GameObject> gameObjects = new List<GameObject>();
        public GameObject prefab;
    }

    [SerializeField] public List<Pool> pools;

   List<GameObjectPools> objectPool;

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
        objectPool = new List<GameObjectPools>();
        foreach(Pool pool in pools)
        {
            GameObjectPools gameObjectList = new GameObjectPools();
            for (int i = 0; i < pool.poolSize; i++)
            {
                
                GameObject enemy = Instantiate(pool.prefab, transform);
                enemy.name = pool.prefab.name;
                gameObjectList.name = enemy.name;
                gameObjectList.prefab = pool.prefab;
                enemy.SetActive(false);
                gameObjectList.gameObjects.Add(enemy);
            }
            objectPool.Add(gameObjectList);
        }

    }

    public GameObject GetPoolObject(string name)
    {

        foreach(GameObject gameObject in findGoodPool(name).gameObjects)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject;
            }

        }

        GameObject newEnemy = Instantiate(findGoodPool(name).prefab, transform);
        newEnemy.SetActive(false);
        findGoodPool(name).gameObjects.Add(newEnemy);
        return newEnemy;

    }

    public GameObjectPools findGoodPool(string name)
    {
        GameObjectPools list = new GameObjectPools();
        foreach(GameObjectPools GOlist in objectPool) 
        {
            if (GOlist.name == name)
            {
                list = GOlist;
            }
        }
        return list;
    }
    public void ResetPool()
    {
        foreach(GameObjectPools lists in objectPool)
        {
            foreach (GameObject obj in lists.gameObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
