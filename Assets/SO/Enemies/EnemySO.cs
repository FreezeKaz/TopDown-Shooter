using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy", order = 51)]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public EntityStat stats;
    public Weapon weapon;
    public GameObject sprite;
   
}
