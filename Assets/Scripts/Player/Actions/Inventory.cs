using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventoryItems;
    public List<Weapon> weapons;

    public void AddItemInInventory(GameObject item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItemInInventory(GameObject item)
    {
        inventoryItems.Remove(item);
    }

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }
}
