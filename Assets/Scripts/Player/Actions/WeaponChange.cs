using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChange : MonoBehaviour
{

    Inventory inventory;
    List<Weapon> weapons;
    public static event Action<Weapon> OnWeaponChange;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        weapons = inventory.GetWeapons();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnWeaponChange?.Invoke(weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnWeaponChange?.Invoke(weapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnWeaponChange?.Invoke(weapons[2]);
        }
    }
}
