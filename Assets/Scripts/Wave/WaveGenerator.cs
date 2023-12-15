using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    // private WaveSO waveData;**

    private static WaveGenerator _instance;
    public static WaveGenerator Instance => _instance;

    public int EnemiesOnField;
    public int TotalEnemies = 0;
    private string mobToSpawn;
    float timer = 0f;
    int indexOfEnemy;
 
    private WaveSO waveData;
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private SpawnPoints spawnPoints;
    [SerializeField] private GameObject enemy;

    private Dictionary<string, int> waveState;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }
    private void InitWaveState()
    {

        waveData = waves[GameManager.Instance.Wave];
        waveState = new Dictionary<string, int>();
        foreach (var element in waveData.enemiesInWave)
        {
            waveState[element.enemySO.enemyName] = element.numberOfEnemies;
        }
        TotalEnemies += waveState.Values.Sum();
    }

    public void StartWave()
    {
        InitWaveState();
        InvokeRepeating("Spawn", 0f, 5f);
    }

    private bool checkIfWaveEnded()
    {
        return waveState.Values.All(item => item == 0);
    }

    private void SpawnSingleEnemy()
    {
        do
        {
            indexOfEnemy = UnityEngine.Random.Range(0, waveState.Count);
            mobToSpawn = waveState.ElementAt(indexOfEnemy).Key;
        } while (waveState[mobToSpawn] == 0);
        waveState[mobToSpawn]--;

        GameObject myEnemy = Instantiate(enemy, spawnPoints.spawnPoints[UnityEngine.Random.Range(0, waveData.SpawnerUsed.Count)].transform.position, Quaternion.identity);
        myEnemy.GetComponent<EnemyManager>().SetStats(waveData.enemiesInWave[indexOfEnemy].enemySO.stats);
        myEnemy.GetComponent<EnemyManager>().SetWeapon(waveData.enemiesInWave[indexOfEnemy].enemySO.weapon);
    }
    private void Spawn()
    {

        if (!checkIfWaveEnded())
        {
            SpawnSingleEnemy();
        }
        else
        {
            CancelInvoke("Spawn");
            InvokeRepeating("CallingNextWave", 0f, 1f);
        }

    }

    private void CallingNextWave()
    {
        timer += 1f;
        Debug.Log(timer);
        if (timer == waveData.TimeToClearAfterEverythingSpawn || TotalEnemies == 0)
        {
            CancelInvoke("CallingNextWave");
            GameManager.Instance.Wave++;
            Debug.Log("Calling wave " + GameManager.Instance.Wave);
            GameManager.Instance.PrepareNextWave(0f);
        }

    }
}


