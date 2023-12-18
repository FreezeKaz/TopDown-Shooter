using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI UpgradeName;
    [SerializeField] public TextMeshProUGUI Description;
    [SerializeField] public Image Image;
    [SerializeField] public UpgradeManager UpgradeManager;

    public UpgradeSO _upgradeData;


    public void OnEnable()
    {
        Debug.Log("I'm showing up");
        Description.text = _upgradeData.Descritpion;
        UpgradeName.text = _upgradeData.Name;
        Image.sprite = _upgradeData.Image;
    }

    public void OnButtonClick()
    {
        
        Debug.Log("upgrade selected");
        UpgradeManager._playerUpgrade.upgradeData = _upgradeData;
        UpgradeManager._playerUpgrade.AddUpgrade();
        UpgradeManager.CardSelected();
    }
}
