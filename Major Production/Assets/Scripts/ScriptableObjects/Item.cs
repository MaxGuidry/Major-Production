using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public Sprite icon = null;
        
        public virtual void UseItem()
        {
            //DO SOMETHING
            Debug.Log("Using " + name);
        }
    }
}