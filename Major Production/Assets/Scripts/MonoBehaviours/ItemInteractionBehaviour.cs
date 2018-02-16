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
    public bool collided = false;

    void Start()
    {
        if (item == (item.Type == ItemType.Stone))
            gameObject.tag = "Stone";

        if (item == (item.Type == ItemType.Wood))
            gameObject.tag = "Wood";

        if (item == (item.Type == ItemType.Chaser))
            GetComponent<Renderer>().material.color = Color.red;
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
        if (other.gameObject.tag == "Player" && !collided)
        {
            collided = true;
            ItemPickedUp.Raise(item);
            var ib = other.gameObject.GetComponent<IStorageable>();
            ib.AddToInventory(item);
            Destroy(gameObject);
        }
    }
}

