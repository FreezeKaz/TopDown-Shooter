using UnityEngine;
[CreateAssetMenu(fileName = "Entity Stat", menuName = "Create new statSet", order = 2)]
public class EntityStat : ScriptableObject
{
    public float HP;
    public float MoveSpeedRatio;
    public float FireRateRatio;
    public float Attack;
    public int XPGiven;
}