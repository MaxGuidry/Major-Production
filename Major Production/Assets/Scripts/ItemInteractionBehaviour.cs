using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;


[Serializable]
public class CollisionEvent : UnityEvent<Item> { }

public class ItemInteractionBehaviour : MonoBehaviour, IDamageable
{
    public Item item;
    public CollisionEvent collisionEvent;
    //private GameObject _player;

    void Start()
    {
        collisionEvent = new CollisionEvent();
        //_player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (_player.GetComponent<InventoryBehaviour>().ActiveInventory.Count == 0)
        //        return;
        //    else if (_player.GetComponent<InventoryBehaviour>().ActiveInventory.Count == 1)
        //    {
        //        _player.GetComponent<InventoryBehaviour>().RemoveFromInventory(item);
        //        GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    }
        //    else
        //    {
        //        _player.GetComponent<InventoryBehaviour>().RemoveFromInventory(
        //            _player.GetComponent<InventoryBehaviour>().ActiveInventory[
        //                _player.GetComponent<InventoryBehaviour>().ActiveInventory.Count - 1]);
        //        GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        collisionEvent.Invoke(item);
        other.GetComponent<InventoryBehaviour>().AddToInventory(item);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}
