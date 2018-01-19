using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects
{
    public enum ItemType
    {
        None = 0,
        Stone = 1,
        Wood = 2,
        Chaser = 3
    }
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public ItemType Type;
        public Sprite icon = null;

        public virtual void UseItem()
        {
            //DO SOMETHING
            Debug.Log("Using " + name);
        }
    }
}