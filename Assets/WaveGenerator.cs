/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
   // private WaveSO waveData;
    public int EnemiesOnField;
    public int TotalEnemies;
    private int mobToSpawn;
    bool WaveReady = false;
   // [SerializeField] private List<WaveSO> waves;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private SpawnPoints spawnPoints;


    private Dictionary<string, int> waveState;

    private void InitWaveState()
    {
        waveData = waves[GameManager.Instance.Wave];
        waveState = new Dictionary<string, int>();
        foreach (var element in waveData.EnemiesUsed)
        {
       *//*     waveState[element.Item1] = element.Item2;*//*
        }
    }

    private void StartWave()
    {
        InitWaveState();
        GetNumberOfEnemies();
    }


    private void GetNumberOfEnemies()
    {
        foreach (var item in waveState.Values)
        {
            TotalEnemies += item;
        }
    }

    private bool checkIfWaveEnded()
    {
        int i = waveState.Values.Count;
        foreach (var item in waveState.Values)
        {
            i += item == 0 ? 1 : 0;
        }
        return i == 3 ? true : false;
    }

    private void Spawn()
    {
        for (int i = 0; i < TotalEnemies; i++)
        {
            foreach (int spawnerID in waveData.SpawnerUsed)
            {

                if (!checkIfWaveEnded())
                {
*//*                    do
                    {
                        mobToSpawn = UnityEngine.Random.Range(0, waveState.Count);
                    } while (waveState[waveData.EnemiesUsed[mobToSpawn].] == 0);
                    waveState[waveData.EnemiesUsed[mobToSpawn].Item1]--;
                    Instantiate(enemies[mobToSpawn], spawnPoints.spawnPoints[UnityEngine.Random.Range(0, waveData.SpawnerUsed.Count)].transform.position, Quaternion.identity);*//*

                }
            }
        }
    }
}
*/