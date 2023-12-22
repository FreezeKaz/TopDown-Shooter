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
        if(_modifiedStat == Entity.Attribute.HP)
        {
            _playerStats.CurrentHP += _playerStats.Stats[_modifiedStat].Value * (float)(_percentage / (float)100);
            _playerStats.CurrentHP = (int)_playerStats.CurrentHP;
        }
        _percentage = upgradeData.percentage;
        _playerStats.Stats[_modifiedStat].AddModifier(AddPercent);
    }
}
