using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryBehaviour : MonoBehaviour, IStorageable
{
    [Space] public Inventory inventory;
    public Dictionary<string, GameObject> objectList;
    [Header("For Viewing Purposes")] public List<Item> ActiveInventory;

    private void Start()
    {
        objectList = new Dictionary<string, GameObject>();
        inventory.StartingInventory = new List<Item>();
        if (inventory.InventoryCap <= 0)
        {
            Debug.LogWarning("Inventory Size must be Greater than 0");
            inventory.InventoryCap = 1;
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
        objectList.Add(newItem.ItemType.ToString() + InventoryText.GetNumberItems(newItem.ItemType), obj);
    }

    public void RemoveFromInventory(Item theItem, GameObject obj)
    {
        if (inventory.CurrentInventory == null) return;
        if (!inventory.CurrentInventory.Contains(theItem)) return;
        inventory.CurrentInventory.Remove(theItem);
       
        switch (theItem.ItemType)
        {
            case ItemType.Wood:
                if (InventoryText.woodAmount <= 0) return;
                objectList.Remove(theItem.ItemType.ToString() + InventoryText.woodAmount);
                InventoryText.woodAmount--;
                InventoryText.WoodAmounttext.text = InventoryText.woodAmount.ToString();
                CheckForGameObject(obj);
                break;

            case ItemType.Stone:
                if (InventoryText.stoneAmount <= 0) return;
                objectList.Remove(theItem.ItemType.ToString() + InventoryText.stoneAmount);
                InventoryText.stoneAmount--;
                InventoryText.StoneAmounttext.text = InventoryText.stoneAmount.ToString();
                CheckForGameObject(obj);
                break;

            case ItemType.Metal:
                if (InventoryText.metalAmount <= 0) return;
                objectList.Remove(theItem.ItemType.ToString() + InventoryText.metalAmount);
                InventoryText.metalAmount--;
                InventoryText.MetalAmounttext.text = InventoryText.metalAmount.ToString();
                CheckForGameObject(obj);
                break;

            case ItemType.Goop:
                if (InventoryText.goopAmount <= 0) return;
                objectList.Remove(theItem.ItemType.ToString() + InventoryText.goopAmount);
                InventoryText.goopAmount--;
                InventoryText.GoopAmounttext.text = InventoryText.goopAmount.ToString();
                CheckForGameObject(obj);
                break;

            case ItemType.None:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ClearInventory(object[] args)
    {
        if (args.Length < 2)
            return;
        if (args[1] as string != "Submit")
            return;
        objectList.Clear();
        foreach (var obj in objectList.Values)
        {
            RemoveAllFromInventory(obj);
        }
    }

    public void RemoveAllFromInventory(GameObject obj)
    {
        CheckForGameObject(obj);
        inventory.CurrentInventory.Clear();
        InventoryText.woodAmount = 0;
        InventoryText.WoodAmounttext.text = InventoryText.woodAmount.ToString();
        InventoryText.stoneAmount = 0;
        InventoryText.StoneAmounttext.text = InventoryText.stoneAmount.ToString();
        InventoryText.metalAmount = 0;
        InventoryText.MetalAmounttext.text = InventoryText.metalAmount.ToString();
        InventoryText.goopAmount = 0;
        InventoryText.GoopAmounttext.text = InventoryText.goopAmount.ToString();
    }

    private void CheckForGameObject(GameObject obj)
    {
        var go = ItemObjectPooler.s_instance.CreateSingle(obj,
            gameObject.transform.position + new Vector3(Random.Range(2, 4), 0, Random.Range(2, 4)),
            Quaternion.identity);
        go.SetActive(true);
    }
}
