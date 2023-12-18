using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "CreateUpgrade", order = 51)]
public class UpgradeSO : ScriptableObject
{
    // Start is called before the first frame update
    public string Name;
    public string Descritpion;
    public Entity.Attribute Stat;
    public int percentage;
}
