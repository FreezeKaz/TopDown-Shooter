using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChange : MonoBehaviour
{

    [SerializeField] Inventory inventory;
    [SerializeField] Shooting shootingScript;
    List<Weapon> weapons;
 

    private void Start()
    {
        weapons = inventory.GetWeapons();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootingScript.ChangeWeapon(weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootingScript.ChangeWeapon(weapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootingScript.ChangeWeapon(weapons[2]);
        }
    }
}
