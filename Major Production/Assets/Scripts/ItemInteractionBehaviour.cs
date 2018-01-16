using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;


[Serializable]
public class CollisionEvent : UnityEvent<Item> { }

public class ItemInteractionBehaviour : MonoBehaviour
{
    public Item item;
    public CollisionEvent collisionEvent;
    void Start()
    {
        collisionEvent = new CollisionEvent();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            collisionEvent.Invoke(item);
            other.gameObject.GetComponent<InventoryBehaviour>().AddToInventory(item);
            Destroy(gameObject);
        }
    }
}
