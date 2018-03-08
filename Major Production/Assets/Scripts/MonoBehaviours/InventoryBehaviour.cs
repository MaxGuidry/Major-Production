using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ScriptableObjects;
using Random = UnityEngine.Random;

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

    public void ClearInventory(object[] args)
    {
        
        if (args.Length < 2)
            return;
        if (args[1] as string != "Submit")
            return;
        RemoveAllFromInventory();
    }
    public void RemoveAllFromInventory()
    {
        
        var items = FindObjectsOfType<ItemInteractionBehaviour>().ToList();

        foreach (var item in ActiveInventory)
        {
            GameObject type = null;

            switch (item.ItemType)
            {
                case ItemType.Wood:
                    foreach (var i in items)
                    {
                        if (i.Item.ItemType == ItemType.Wood)
                        {
                            type = i.gameObject;
                            break;

                        }
                            
                    }

                    break;
                case ItemType.Metal:

                    foreach (var i in items)
                    {
                        if (i.Item.ItemType == ItemType.Metal)
                        {
                            type = i.gameObject;
                            break;

                        }

                    }

                    break;
                case ItemType.Goop:
                    foreach (var i in items)
                    {
                        if (i.Item.ItemType == ItemType.Goop)
                        {
                            type = i.gameObject;
                            break;

                        }

                    }

                    break;
                case ItemType.Stone:
                    foreach (var i in items)
                    {
                        if (i.Item.ItemType == ItemType.Stone)
                        {
                            type = i.gameObject;
                            break;

                        }

                    }

                    break;
            }
            if(!type)
                return;

            var go = ItemObjectPooler.s_instance.Create(type,
                this.gameObject.transform.position + new Vector3(Random.Range(2, 4), 0, Random.Range(2, 4)),
                Quaternion.identity);

            go.SetActive(true);
        }
        //if (inventory.CurrentInventory == null) return;
        inventory.CurrentInventory.Clear();
        InventoryText.woodAmount=0;
        InventoryText.WoodAmounttext.text = InventoryText.woodAmount.ToString();
        InventoryText.stoneAmount=0;
        InventoryText.StoneAmounttext.text = InventoryText.stoneAmount.ToString();
        InventoryText.metalAmount=0;
        InventoryText.MetalAmounttext.text = InventoryText.metalAmount.ToString();
        InventoryText.goopAmount=0;
        InventoryText.GoopAmounttext.text = InventoryText.goopAmount.ToString();
        

    }
}
