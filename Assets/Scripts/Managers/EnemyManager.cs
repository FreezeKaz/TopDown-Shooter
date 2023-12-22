using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] public GameObject Actions;
    [SerializeField] public GameObject Physics;
    [SerializeField] public GameObject Render;

    [SerializeField] public Entity myEntityStats;
    [SerializeField] public Shooting myShootingScript;
    

    public void OnDisable()
    {
        myShootingScript.StopShooting();
    }
}