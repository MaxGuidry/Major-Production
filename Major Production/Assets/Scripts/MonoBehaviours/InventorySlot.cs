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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (inventoryBehaviour.GetComponent<Transform>().tag)
            {
                case "P1":
                    if(inventoryBehaviour.P1.inInventory)
                        DropItem(); 
                    break;

                case "P2":
                    if (inventoryBehaviour.P2.inInventory)
                        DropItem();
                    break;

                case "P3":
                    if (inventoryBehaviour.P3.inInventory)
                        DropItem();
                    break;

                case "P4":
                    if (inventoryBehaviour.P4.inInventory)
                        DropItem();
                    break;
            }
        }
    }

    public void DropItem()
    {
        if (item == null) return;
        Debug.Log("Droping: " + item.name);
        switch (inventoryBehaviour.GetComponent<Transform>().tag)
        {
            case "P1":
                if (inventoryBehaviour.inventory.CurrentInventory.Contains(item))
                    inventoryBehaviour.RemoveFromInventory(item, inventoryBehaviour.objectList[item.ItemType.ToString() + inventoryBehaviour.P1.GetNumberItems(item.ItemType)]);
                break;

            case "P2":
                if (inventoryBehaviour.inventory.CurrentInventory.Contains(item))
                    inventoryBehaviour.RemoveFromInventory(item, inventoryBehaviour.objectList[item.ItemType.ToString() + inventoryBehaviour.P2.GetNumberItems(item.ItemType)]);
                break;

            case "P3":
                if (inventoryBehaviour.inventory.CurrentInventory.Contains(item))
                    inventoryBehaviour.RemoveFromInventory(item, inventoryBehaviour.objectList[item.ItemType.ToString() + inventoryBehaviour.P3.GetNumberItems(item.ItemType)]);
                break;

            case "P4":
                if (inventoryBehaviour.inventory.CurrentInventory.Contains(item))
                    inventoryBehaviour.RemoveFromInventory(item, inventoryBehaviour.objectList[item.ItemType.ToString() + inventoryBehaviour.P4.GetNumberItems(item.ItemType)]);
                break;
        }
    }
}
