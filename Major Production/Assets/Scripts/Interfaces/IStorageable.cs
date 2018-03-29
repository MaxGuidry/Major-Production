using ScriptableObjects;
using UnityEngine;

public interface IStorageable
{
    void AddToInventory(Item theItem, GameObject obj);
    void RemoveFromInventory(Item theItem, GameObject obj);
    void RemoveAllFromInventory(GameObject obj, InventoryText player);
}
