using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    private static WaveGenerator _instance;
    public static WaveGenerator Instance => _instance;

    public int TotalEnemies = 0;
    private string mobToSpawn;
    public float timer = 0f;
    int indexOfEnemy;

    private WaveSO waveData;
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private SpawnPoints spawnPoints;
    [SerializeField] public List<Transform> WayPoints; //get them from the ennemy

    public GameObject _playerSpawnPoint; //get them from the ennemy

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
        Debug.Log("Okay");
       
    }

    private void Start()
    {
        MapManager.Instance.ChangeMap(waves[0].map);
    }
    private void InitWaveState()
    {
        waveData = waves[GameManager.Instance.Wave];
        waveState = new Dictionary<string, int>();
        foreach (var element in waveData.enemiesInWave)
        {
            waveState[element.enemyUsed.name] = element.numberOfEnemies;   
        }
        TotalEnemies += waveState.Values.Sum();
    }

    private void InitMap()
    {

        if (waves[GameManager.Instance.Wave].ChangeMap)
        {
            MapManager.Instance.ChangeMap(waves[GameManager.Instance.Wave].map);
        }
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
            //Debug.Log("there's " + index + "left of ennemies type ");
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

        myEnemy.transform.position = MapManager.Instance.map.spawnPoints.spawnPoints[UnityEngine.Random.Range(0, waveData.SpawnerUsed.Count)].transform.position;

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
      

        if(!waveData.BossRoom && !CheckIfNextWaveIsBoss())
        {
            while (timer < waveData.TimeToClearAfterEverythingSpawn && TotalEnemies != 0)
            {
                timer += Time.deltaTime;
                Debug.Log("Waiting for " + (waveData.TimeToClearAfterEverythingSpawn - timer) + " seconds to start the next wave...");

                if (TotalEnemies == 0)
                {
                    break;
                }
                yield return null;
            }
        }
        else
        {
            while(TotalEnemies > 0)
            {
                yield return null;
            }
        }

        Debug.Log("Spawning next wave");
        GameManager.Instance.Wave++;
        if (GameManager.Instance.Wave >= waves.Count)
        {
            GameManager.Instance.Wave = 0; //resetting wave count
        }
        Debug.Log("Calling wave " + GameManager.Instance.Wave);
        InitMap();
        GameManager.Instance.PrepareNextWave(0f);
    }

    private bool CheckIfNextWaveIsBoss()
    {
        bool nextRoomBoss = waves.ElementAtOrDefault(GameManager.Instance.Wave + 1)?.BossRoom ?? false;
        return nextRoomBoss;
    }
    public void StopWaves()
    {
        Debug.Log("Stop");
        StopAllCoroutines();
        GameManager.Instance.Wave = 0;
        timer = 0f;
        TotalEnemies = 0;
    }
}


