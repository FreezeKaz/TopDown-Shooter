using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _cardPanel;
    [SerializeField] private List<CardManager> _cards;
    [SerializeField] private List<UpgradeSO> allUpgrades;
    [SerializeField] public Upgrade _playerUpgrade;


    public void Awake()
    {
        _cardPanel.SetActive(false);
    }
    public void OnLevelUp()
    {
      

        List<UpgradeSO> choosenUpgrades = SelectRandomItems(allUpgrades, 3);
        Debug.Log("dispalying");
        _cardPanel.SetActive(true);
        for (int i = 0; i < choosenUpgrades.Count; i++)
        {
            _cards[i]._upgradeData = choosenUpgrades[i];
        }
        Time.timeScale = 0f;
    }

    public void CardSelected()
    {
        Debug.Log("not dispalying");
        _cardPanel.SetActive(false);
        Time.timeScale = 1.0f; //pause the game
    }
    
    static List<T> SelectRandomItems<T>(List<T> list, int count)
    {
        List<T> shuffledList = list.OrderBy(x => Guid.NewGuid()).ToList();
        return shuffledList.Take(count).ToList();
    }
}
