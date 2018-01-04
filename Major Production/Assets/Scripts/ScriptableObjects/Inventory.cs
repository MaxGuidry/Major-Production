using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
    public class Inventory : ScriptableObject
    {
        public int InventoryCap = 10;

        public List<Item> StartingInventory = new List<Item>();

        public List<Item> CurrentInventory
        {
            get { return StartingInventory; }
        }
    }
}