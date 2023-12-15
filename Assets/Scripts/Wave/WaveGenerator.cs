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
        StartCoroutine(Spawn());
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

        GameObject myEnemy = EnemyPoolManager.Instance.GetPoolObject();
        myEnemy.SetActive(true);
        myEnemy.transform.position = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, waveData.SpawnerUsed.Count)].transform.position;
        myEnemy.GetComponent<EnemyManager>().SetStats(waveData.enemiesInWave[indexOfEnemy].enemySO.stats);
        myEnemy.GetComponent<EnemyManager>().SetWeapon(waveData.enemiesInWave[indexOfEnemy].enemySO.weapon);
    }
    IEnumerator Spawn()
    {

        while(!checkIfWaveEnded())
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(waveData.SpawnDelay);
        }    

            StartCoroutine(CallingNextWave());
    
    }

    IEnumerator CallingNextWave()
    {
        while (timer != waveData.TimeToClearAfterEverythingSpawn && TotalEnemies != 0)
        {
            timer += 1f;
            Debug.Log(timer);
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.Wave++;
        Debug.Log("Calling wave " + GameManager.Instance.Wave);
        GameManager.Instance.PrepareNextWave(0f);
    }
}


