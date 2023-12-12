using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public float FireRate = 4;
    public ScriptableObject bullet;
}
