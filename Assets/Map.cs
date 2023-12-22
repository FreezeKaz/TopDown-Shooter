using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public SpawnPoints spawnPoints;
    [SerializeField] public GameObject playerSpawnPoint;
    [SerializeField] public WayPoints wayPoints;
}
