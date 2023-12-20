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

    private UpgradeSO _upgradeData;


    
    public void OnButtonClick()
    {
        
        Debug.Log("upgrade selected");
        UpgradeManager._playerUpgrade.upgradeData = _upgradeData;
        UpgradeManager._playerUpgrade.AddUpgrade();
        UpgradeManager.CardSelected();
    }

    public void SetData(UpgradeSO upgradeData)
    {

        _upgradeData = upgradeData;
        UpgradeName.text = _upgradeData.Name;
        Description.text = _upgradeData.Descritpion;

        Debug.Log("setting data for " +  _upgradeData.Name);
        Debug.Log(UpgradeName.text + Description.text);
    }
}
