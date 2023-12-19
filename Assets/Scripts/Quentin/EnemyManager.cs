using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject Actions;
    public GameObject Physics;
    public GameObject EntityBehaviour;

    [SerializeField] Entity myEntityStats;
    [SerializeField] Shooting myShootingScript;
    

    public void OnDisable()
    {
        myShootingScript.StopShooting();
    }
}