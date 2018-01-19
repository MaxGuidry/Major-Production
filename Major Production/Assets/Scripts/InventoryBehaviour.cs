using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class OnInvChange : UnityEvent<Inventory> { }

public class InventoryBehaviour : MonoBehaviour, IStorageable
{
    [Space]
    public Inventory inventory;
    [Header("For Viewing Purposes")]
    public List<Item> ActiveInventory;

    public InventoryUIBehaviour InventoryUi;
    public OnInvChange InvChange;

    [HideInInspector]
    public List<ItemType> Stones;
    [HideInInspector]
    public List<ItemType> Wood;
    void Start()
    {
        InvChange = new OnInvChange();
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
        InventoryUi.UpdateUI();
        InvChange.Invoke(inventory);

        switch (newItem.Type)
        {
            case ItemType.Stone:
                Stones.Add(newItem.Type);
                break;
            case ItemType.Wood:
                Wood.Add(newItem.Type);
                break;
            case ItemType.Chaser:
                SceneManager.LoadScene("100.Inventory");
                break;
            case ItemType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Debug.Log("Item Added: " + newItem.Type);
    }

    public void RemoveFromInventory(Item theItem)
    {
        if (inventory.CurrentInventory == null) return;
        if (!inventory.CurrentInventory.Contains(theItem)) return;
        inventory.CurrentInventory.Remove(theItem);
        InvChange.Invoke(inventory);

        switch (theItem.Type)
        {
            case ItemType.Stone:
                Stones.Remove(theItem.Type);
                break;
            case ItemType.Wood:
                Wood.Remove(theItem.Type);
                break;
            case ItemType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        Debug.Log("Removed Item: " + theItem.Type);
    }

    public void RemoveAllFromInventory()
    {
        if (inventory.CurrentInventory == null) return;
        inventory.CurrentInventory.Clear();
        Stones.Clear();
        Wood.Clear();
    }
}
