using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehaviour : MonoBehaviour, IStorageable
{
    [Space] public Inventory inventory;
    public Dictionary<string, GameObject> objectList, objectList1, objectList2, objectList3;
    [Header("For Viewing Purposes")] public List<Item> ActiveInventory;
    public Transform dropPosition;

    public InventoryText P1, P2, P3, P4;

    private void Start()
    {
        objectList = new Dictionary<string, GameObject>();
        objectList1 = new Dictionary<string, GameObject>();
        objectList2 = new Dictionary<string, GameObject>();
        objectList3 = new Dictionary<string, GameObject>();
        inventory.StartingInventory = new List<Item>();
        if (inventory.InventoryCap <= 0)
        {
            Debug.LogWarning("Inventory Size must be Greater than 0");
            inventory.InventoryCap = 1;
        }

        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                P1 = gameObject.transform.parent.GetComponentInChildren<InventoryText>();
                P2 = null;
                P3 = null;
                P4 = null;
                break;
            case "P2":
                P1 = null;
                P2 = gameObject.transform.parent.GetComponentInChildren<InventoryText>();
                P3 = null;
                P4 = null;
                break;
            case "P3":
                P1 = null;
                P2 = null;
                P3 = gameObject.transform.parent.GetComponentInChildren<InventoryText>();
                P4 = null;
                break;
            case "P4":
                P1 = null;
                P2 = null;
                P3 = null;
                P4 = gameObject.transform.parent.GetComponentInChildren<InventoryText>();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        ActiveInventory = inventory.CurrentInventory;
    }

    public void AddToInventory(Item newItem, GameObject obj)
    {
        if (inventory.CurrentInventory.Count >= inventory.InventoryCap) return;
        inventory.CurrentInventory.Add(newItem);

        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                objectList.Add(newItem.ItemType.ToString() + P1.GetNumberItems(newItem.ItemType), obj);
                break;
            case "P2":
                objectList1.Add(newItem.ItemType.ToString() + P2.GetNumberItems(newItem.ItemType), obj);
                break;
            case "P3":
                objectList2.Add(newItem.ItemType.ToString() + P3.GetNumberItems(newItem.ItemType), obj);
                break;
            case "P4":
                objectList3.Add(newItem.ItemType.ToString() + P4.GetNumberItems(newItem.ItemType), obj);
                break;
            default:
                break;
        }

    }

    private void RemoveItem(uint amount, Text itemText, Item theItem)
    {
        if (amount <= 0) return;
        objectList.Remove(theItem.ItemType.ToString() + amount);
        amount--;
        itemText.text = amount.ToString();
    }

    private void SwitchCheck(InventoryText player, Item theItem, GameObject obj)
    {
        switch (theItem.ItemType)
        {
            case ItemType.None:
                break;
            case ItemType.Wood:
                RemoveItem(player.woodAmount, player.WoodAmounttext, theItem);
                CheckForGameObject(obj);
                break;
            case ItemType.Stone:
                RemoveItem(player.stoneAmount, player.StoneAmounttext, theItem);
                CheckForGameObject(obj);
                break;
            case ItemType.Metal:
                RemoveItem(player.metalAmount, player.MetalAmounttext, theItem);
                CheckForGameObject(obj);
                break;
            case ItemType.Goop:
                RemoveItem(player.goopAmount, player.GoopAmounttext, theItem);
                CheckForGameObject(obj);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RemoveFromInventory(Item theItem, GameObject obj)
    {
        if (inventory.CurrentInventory == null) return;
        if (!inventory.CurrentInventory.Contains(theItem)) return;
        inventory.CurrentInventory.Remove(theItem);

        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                SwitchCheck(P1, theItem, obj);
                break;
            case "P2":
                SwitchCheck(P2, theItem, obj);
                break;
            case "P3":
                SwitchCheck(P3, theItem, obj);
                break;
            case "P4":
                SwitchCheck(P4, theItem, obj);
                break;
            default:
                break;
        }
    }

    public void ClearInventory(object[] args)
   {
    //    if (args.Length < 2)
    //        return;

    //    switch (args[1] as string)
    //    {
    //        case "Right Stick Button":
    //            break;
    //        case "Right Stick Button1":
    //            break;
    //        case "Right Stick Button2":
    //            break;
    //        case "Right Stick Button3":
    //            break;
    //        default:
    //           return;
    //    }

    //    switch (GetComponent<Transform>().tag)
    //    {
    //        case "P1":
    //            foreach (var obj in objectList.Values)
    //            {
    //                RemoveAllFromInventory(obj, P1);
    //            }
    //            break;
    //        case "P2":
    //            foreach (var obj in objectList1.Values)
    //            {
    //                RemoveAllFromInventory(obj, P2);
    //            }
    //            break;
    //        case "P3":
    //            foreach (var obj in objectList2.Values)
    //            {
    //                RemoveAllFromInventory(obj, P3);
    //            }
    //            break;
    //        case "P4":
    //            foreach (var obj in objectList3.Values)
    //            {
    //                RemoveAllFromInventory(obj, P4);
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //    objectList.Clear();
    }

    public void RemoveAllFromInventory(GameObject obj, InventoryText player)
    {
    //    CheckForGameObject(obj);
    //    inventory.CurrentInventory.Clear();

    //    player.woodAmount = 0;
    //    player.WoodAmounttext.text = player.woodAmount.ToString();
    //    player.stoneAmount = 0;
    //    player.StoneAmounttext.text = player.stoneAmount.ToString();
    //    player.metalAmount = 0;
    //    player.MetalAmounttext.text = player.metalAmount.ToString();
    //    player.goopAmount = 0;
    //    player.GoopAmounttext.text = player.goopAmount.ToString();
    }

    private void CheckForGameObject(GameObject obj)
    {
        var go = ItemObjectPooler.s_instance.CreateSingle(obj,
            dropPosition.position,
            Quaternion.identity);
        go.SetActive(true);
    }
}
