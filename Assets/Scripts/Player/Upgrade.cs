using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeSO upgradeData;
    [SerializeField] Entity _playerStats;
    private int _percentage;
    private Entity.Attribute _modifiedStat;


    private float AddPercent(float value) => value + value * (float)(_percentage / (float)100);


    public void AddUpgrade()
    {
        _modifiedStat = upgradeData.Stat;
        _percentage = upgradeData.percentage;
        _playerStats.Stats[_modifiedStat].AddModifier(AddPercent);
    }
}
