using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public float FireRate = 4;
    public GameObject bullet;
    public List<int> firePoints;
}
