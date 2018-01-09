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
    //private GameObject _player;

    void Start()
    {
        collisionEvent = new CollisionEvent();
        //_player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        collisionEvent.Invoke(item);
        other.GetComponent<InventoryBehaviour>().AddToInventory(item);
        Destroy(gameObject);
    }
}
