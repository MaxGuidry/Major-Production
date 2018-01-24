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
        if (item == (item.Type == ItemType.Stone))
        {
            GetComponent<Renderer>().material.color = Color.grey;
            gameObject.tag = "Stone";
        }

        if (item == (item.Type == ItemType.Wood))
        {
            GetComponent<Renderer>().material.color = Color.green;
            gameObject.tag = "Wood";
        }

        if (item == (item.Type == ItemType.Chaser))
            GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisionEvent.Invoke(item);
            other.gameObject.GetComponent<InventoryBehaviour>().AddToInventory(item);
            Destroy(gameObject);
        }
    }
}
