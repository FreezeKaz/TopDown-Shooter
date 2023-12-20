using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager _instance;
    public static BulletPoolManager Instance => _instance;


    [SerializeField] private GameObject bulletPrefab;
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
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            objectPool.Add(bullet);
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
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        newBullet.SetActive(false);
        objectPool.Add(newBullet);

        return newBullet;
    }

    public void ResetPool()
    {
        foreach (GameObject obj in objectPool)
        {
            obj.SetActive(false);
        }
    }

}
