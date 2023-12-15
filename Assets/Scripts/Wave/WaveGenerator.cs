using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    // private WaveSO waveData;
    public int EnemiesOnField;
    public int TotalEnemies;
    private string mobToSpawn;
    int indexOfEnemy;
    bool WaveReady = false;
    private WaveSO waveData;
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private SpawnPoints spawnPoints;
    [SerializeField] private GameObject enemy;

    private Dictionary<string, int> waveState;


    private void Awake()
    {
        StartWave();
    }
    private void InitWaveState()
    {
        waveData = waves[GameManager.Instance.Wave];
        waveState = new Dictionary<string, int>();
        foreach (var element in waveData.enemiesInWave)
        {
            waveState[element.enemySO.enemyName] = element.numberOfEnemies;
        }
    }



    private void StartWave()
    {
        InitWaveState();
        GetNumberOfEnemies();
        InvokeRepeating("Spawn", 0f, 5f);
    }


    private void GetNumberOfEnemies()
    {
        foreach (var item in waveState.Values)
        {
            TotalEnemies += item;
            Debug.Log(TotalEnemies);
        }
    }

    private bool checkIfWaveEnded()
    {
        int i = 0;
        foreach (var item in waveState.Values)
        {
            i += item == 0 ? 1 : 0;
        }
        return i == waveState.Values.Count ? true : false;
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
        else CancelInvoke("Spawn");

      

    }
}


