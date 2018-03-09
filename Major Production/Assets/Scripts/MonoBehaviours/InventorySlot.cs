using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item item;
    private InventoryBehaviour inventoryBehaviour;
    private void Start()
    {
        if (item == null) return;
        icon.sprite = item.icon;
        icon.enabled = true;
        inventoryBehaviour = FindObjectOfType<InventoryBehaviour>();
    }

    public void DropItem()
    {
        if (item == null) return;
        Debug.Log("Droping: " + item.name);
        if (inventoryBehaviour.inventory.CurrentInventory.Contains(item))
            inventoryBehaviour.RemoveFromInventory(item, inventoryBehaviour.objectList[item.ItemType.ToString() + InventoryText.GetNumberItems(item.ItemType)]);
    }
}
