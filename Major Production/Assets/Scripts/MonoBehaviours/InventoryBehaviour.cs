using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InventoryBehaviour : MonoBehaviour, IStorageable
{
    [Space]
    public Inventory inventory;
    [Header("For Viewing Purposes")]
    public List<Item> ActiveInventory;

    void Start()
    {
        inventory.StartingInventory = new List<Item>();
        if (inventory.InventoryCap <= 0)
        {
            Debug.LogWarning("Inventory Size must be Greater than 0");
            inventory.InventoryCap = 1;
        }
    }

    void Update()
    {
        ActiveInventory = inventory.CurrentInventory;
    }

    public void AddToInventory(Item newItem)
    {
        if (inventory.CurrentInventory.Count >= inventory.InventoryCap) return;
        inventory.CurrentInventory.Add(newItem);
    }

    public void RemoveFromInventory(Item theItem)
    {
        if (inventory.CurrentInventory == null) return;
        if (!inventory.CurrentInventory.Contains(theItem)) return;
        inventory.CurrentInventory.Remove(theItem);
    }

    public void RemoveAllFromInventory()
    {
        if (inventory.CurrentInventory == null) return;
        inventory.CurrentInventory.Clear();

    }
}
