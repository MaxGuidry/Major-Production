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
            switch (Item.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Wood:
                    InventoryText.woodAmount++;
                    InventoryText.WoodAmounttext.text = InventoryText.woodAmount.ToString();
                    var tweenItWood = InventoryText.WoodAmounttext.GetComponent<TweenScaleBehaviour>();
                    tweenItWood.TweenScale();
                    break;
                case ItemType.Stone:
                    InventoryText.stoneAmount++;
                    InventoryText.StoneAmounttext.text = InventoryText.stoneAmount.ToString();
                    var tweenItStone = InventoryText.StoneAmounttext.GetComponent<TweenScaleBehaviour>();
                    tweenItStone.TweenScale();
                    break;
                case ItemType.Metal:
                    InventoryText.metalAmount++;
                    InventoryText.MetalAmounttext.text = InventoryText.metalAmount.ToString();
                    var tweenItMetal = InventoryText.MetalAmounttext.GetComponent<TweenScaleBehaviour>();
                    tweenItMetal.TweenScale();
                    break;
                case ItemType.Goop:
                    InventoryText.goopAmount++;
                    InventoryText.GoopAmounttext.text = InventoryText.goopAmount.ToString();
                    var tweenItGoop = InventoryText.GoopAmounttext.GetComponent<TweenScaleBehaviour>();
                    tweenItGoop.TweenScale();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ItemObjectPooler.s_instance.Destroy(gameObject);
        }
    }
}

