using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;


public class ItemInteractionBehaviour : MonoBehaviour
{
    public float SpoilerTime;
    public Item Item;
    public GameEventArgs ItemPickedUp;

    private void Update()
    {
        var timer = 0f;
        timer += Time.time;
        if (SpoilerTime <= 0)
            return;
        if (timer > SpoilerTime)
            ItemObjectPooler.s_instance.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemPickedUp.Raise(Item);
            var ib = other.gameObject.GetComponent<IStorageable>();
            ib.AddToInventory(Item);
            ItemObjectPooler.s_instance.Destroy(gameObject);
        }
    }
}

