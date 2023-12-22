using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _cardPanel;
    [SerializeField] private List<CardManager> _cards;
    [SerializeField] private List<UpgradeSO> allUpgrades;
    [SerializeField] public Upgrade _playerUpgrade;
    public int ChoseUpgrade = 0;


    public void Start()
    {
        _cardPanel.SetActive(false);
    }
    public void OnLevelUp()
    {
        ChoseUpgrade = 1;
        List<UpgradeSO> choosenUpgrades = SelectRandomItems(allUpgrades, 3);
        Debug.Log("dispalying");
        _cardPanel.SetActive(true);

        for (int i = 0; i < choosenUpgrades.Count; i++)
        {
            Debug.Log(choosenUpgrades[i].Name);
            _cards[i].SetData(choosenUpgrades[i]);
            _cards[i].Description.text = choosenUpgrades[i].Descritpion;
            _cards[i].UpgradeName.text = choosenUpgrades[i].Name;
            _cards[i].Image.sprite = choosenUpgrades[i].Image;
        }
        Time.timeScale = 0f;
    }

    public void CardSelected()
    {
        Debug.Log("not dispalying");
        ChoseUpgrade = 2;
        _cardPanel.SetActive(false);
        Time.timeScale = 1.0f; //pause the game
    }

    static List<T> SelectRandomItems<T>(List<T> list, int count)
        => list.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
}
