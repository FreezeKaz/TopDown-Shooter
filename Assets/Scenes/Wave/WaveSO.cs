using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;


[CreateAssetMenu(fileName = "Wave", menuName = "Create Wave", order = 1)]
public class Wave : ScriptableObject
{
    public Dictionary<string, int> enemies;
}

