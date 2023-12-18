using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeSO upgradeData;
    [SerializeField] Entity _playerStats;
    private int _percentage;
    private Entity.Attribute _modifiedStat;


    private float AddPercent(float value) => value * (1 + (_percentage / 100));


    public void AddUpgrade()
    {

        _playerStats.Stats[_modifiedStat].AddModifier(AddPercent);
    }
}
