using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item item;

    private void Start()
    {
        if (item == null) return;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void UseItem()
    {
        if(item == null) return;
        item.UseItem();
    }
}
