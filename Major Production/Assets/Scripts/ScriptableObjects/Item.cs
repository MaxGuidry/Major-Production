using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Wood,
    Stone,
    Metal,
    Goop
}
namespace ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public Sprite icon = null;
        public ItemType ItemType;
    }
}