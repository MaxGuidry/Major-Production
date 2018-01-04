using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

public class InventoryBehaviour : MonoBehaviour
{
    public Inventory inventory;

    void Start()
    {
        if (inventory.InventoryCap <= 0)
        {
            Debug.LogWarning("Inventory Size must be Greater than 0");
            inventory.InventoryCap = 1;
        }
    }

    public void AddToInventory(Item newItem)
    {
        Debug.Log("Item Added: " + newItem.Type);

        var currentItem = GetItem(newItem.Type);


        
    }

    public void RemoveFromInventory(Item newItem)
    {

    }

    public Item GetItem(ItemType type)
    {
        for (var i = 0; i < inventory.InventoryCap; i++)
        {
            if (inventory.CurrentInventory[i].Type == type)
            {
                return inventory.CurrentInventory[i];
            }
        }
        var nullitem = new Item { Type = ItemType.None };
        return nullitem;
    }

    private int FindFreeItemSlot()
    {
        for (var i = 0; i < inventory.InventoryCap; i++)
        {
            if (inventory.CurrentInventory[i].Type == ItemType.None)
                return i;
        }
        return -1;
    }
}
