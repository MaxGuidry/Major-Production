using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    private Item item;

    public void UIItemManager(Item newItem, bool AddItem)
    {
        if (AddItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else if (!AddItem)
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void UseItem()
    {
        if(item == null) return;
        item.UseItem();
    }
}
