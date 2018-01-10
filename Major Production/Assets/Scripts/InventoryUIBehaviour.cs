using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class InventoryUIBehaviour : MonoBehaviour {

    public InventoryBehaviour inventory;

    public Transform itemsParent;

    private InventorySlot[] slots;
    // Use this for initialization
    void Start ()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (var i = 0; i < slots.Length; i++)
        {
            if (i < inventory.ActiveInventory.Count)
            {
                slots[i].AddItem(inventory.ActiveInventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
