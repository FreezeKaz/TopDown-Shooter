using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    private static WaveGenerator _instance;
    public static WaveGenerator Instance => _instance;

    public int TotalEnemies = 0;
    private string mobToSpawn;
    float timer = 0f;
    int indexOfEnemy;

    private WaveSO waveData;
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private SpawnPoints spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] public List<Transform> WayPoints; //get them from the ennemy

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
            waveState[element.enemyUsed.name] = element.numberOfEnemies;   
        }
        Debug.Log(TotalEnemies + " + " + waveState.Values.Sum());
        TotalEnemies += waveState.Values.Sum();
        Debug.Log(TotalEnemies);
    }

    public void StartWave()
    {
        InitWaveState();
        StartCoroutine(Spawn());
    }

    private bool checkIfWaveEnded()
    {
        foreach (int index in waveState.Values)
        {
            Debug.Log("there's " + index + "left of ennemies type ");
        }
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

        GameObject myEnemy = EnemyPoolManager.Instance.GetPoolObject(mobToSpawn);
        myEnemy.SetActive(true);
        SetDefaultPoolEnemy(myEnemy);
    }

    private void SetDefaultPoolEnemy(GameObject myEnemy)
    {
        Destroy(myEnemy.GetComponent<IABT>());
        //Destroy(myEnemy.GetComponent<BaseEnemy>());
        myEnemy.transform.position = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, waveData.SpawnerUsed.Count)].transform.position;
        var newEnemyIA = myEnemy.AddComponent<IABT>();
        newEnemyIA.waypoints = WayPoints;
        //var newEnemyIA = myEnemy.AddComponent<BaseEnemy>();


    }
    IEnumerator Spawn()
    {

        while (!checkIfWaveEnded())
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(waveData.SpawnDelay);
        }
        Debug.Log("wave has finished spawning");
        StartCoroutine(CallingNextWave());

    }

    IEnumerator CallingNextWave()
    {
        timer = 0f;
        while (timer < waveData.TimeToClearAfterEverythingSpawn && TotalEnemies != 0)
        {
            timer += Time.deltaTime;
            Debug.Log("Waiting for " + (waveData.TimeToClearAfterEverythingSpawn - timer) + " seconds to start the next wave...");

            if (TotalEnemies == 0)
            {
                // If all enemies are defeated before the time is up, break the loop
                break;
            }
            yield return null;
        }
        GameManager.Instance.Wave++;
        if (GameManager.Instance.Wave >= waves.Count)
        {
            GameManager.Instance.Wave = 0;
        }
        Debug.Log("Calling wave " + GameManager.Instance.Wave);
        GameManager.Instance.PrepareNextWave(0f);
    }

    public void StopWaves()
    {
        StopAllCoroutines();
        GameManager.Instance.Wave = 0;
        timer = 0f;
        TotalEnemies = 0;
    }
}


