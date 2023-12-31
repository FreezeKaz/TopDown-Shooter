using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using System;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave Scriptable Object", order = 51)]
public class WaveSO : ScriptableObject
{
    [System.Serializable]
    public class EnemyWave
    {
        public GameObject enemyUsed;
        public int numberOfEnemies;
    }
    public List<GameObject> SpawnerUsed;
    public List<EnemyWave> enemiesInWave;

    public float SpawnDelay;
    public float TimeToClearAfterEverythingSpawn;

    public bool BossRoom;
    public bool ChangeMap;
    public GameObject map;
    //public BossSO
}