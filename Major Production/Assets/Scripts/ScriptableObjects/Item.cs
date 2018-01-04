using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects
{
    public enum ItemType
    {
        None = 0,
        Stone = 1,
        Wood = 2
    }
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public ItemType Type;
    }
}