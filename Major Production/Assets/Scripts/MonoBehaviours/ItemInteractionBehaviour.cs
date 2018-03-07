using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;


public class ItemInteractionBehaviour : MonoBehaviour
{
    public float SpoilerTime;
    public Item item;
    public GameEventArgs ItemPickedUp;
    private ItemObjectPooler pooler;

    private void Start()
    {
        pooler = FindObjectOfType<ItemObjectPooler>();
    }

    private void Update()
    {
        Spoiler();
    }

    public void Spoiler()
    {
        var timer = 0f;
        timer += Time.time;
        if (SpoilerTime <= 0)
            return;
        if (timer > SpoilerTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemPickedUp.Raise(item);
            var ib = other.gameObject.GetComponent<IStorageable>();
            ib.AddToInventory(item);
            pooler.Destroy(gameObject);
        }
    }
}

