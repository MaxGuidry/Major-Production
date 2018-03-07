using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    private int GoopAmount;
    public Text GoopAmounttext;
    private InventoryBehaviour Inventory;

    private int MetalAmount;
    public Text MetalAmounttext;
    private int StoneAmount;
    public Text StoneAmounttext;
    private int WoodAmount;
    public Text WoodAmounttext;
    private List<ScriptableObjects.Item> WoodList = new List<ScriptableObjects.Item>();
    private List<ScriptableObjects.Item> StoneList = new List<ScriptableObjects.Item>();
    private List<ScriptableObjects.Item> MetalList = new List<ScriptableObjects.Item>();
    private List<ScriptableObjects.Item> GoopList = new List<ScriptableObjects.Item>();

    private int prevWood;
    private int prevStone;
    private int prevMetal;
    private int prevGoop;

    // Use this for initialization
    private void Start()
    {
        Inventory = FindObjectOfType<InventoryBehaviour>();
    }

    /// <summary>
    ///     Checks each item in inventory then makes a number
    ///     that is equal to the amount in inventory
    /// </summary>
    public void CheckInv()
    {
        WoodAmount = 0;
        StoneAmount = 0;
        MetalAmount = 0;
        GoopAmount = 0;

        WoodList.Clear();
        StoneList.Clear();
        MetalList.Clear();
        GoopList.Clear();

        prevWood = 0;
        prevStone = 0;
        prevMetal = 0;
        prevGoop = 0;
      
        foreach (var item in Inventory.ActiveInventory)
        {
            switch (item.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Wood:
                    ItemAdded(WoodAmount, WoodAmounttext, WoodList, item);
                    break;
                case ItemType.Stone:
                    ItemAdded(StoneAmount, StoneAmounttext, StoneList, item);
                    break;
                case ItemType.Metal:
                    ItemAdded(MetalAmount, MetalAmounttext, MetalList, item);
                    break;
                case ItemType.Goop:
                    ItemAdded(GoopAmount, GoopAmounttext, GoopList, item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (WoodList.Count > prevWood)
        {
            prevWood = WoodList.Count;
            StartCoroutine(TextPop(WoodAmounttext));
        }

        if (StoneList.Count > prevStone)
        {
            prevStone = StoneList.Count;
            StartCoroutine(TextPop(StoneAmounttext));
        }

        if (MetalList.Count > prevMetal)
        {
            prevMetal = MetalList.Count;
            StartCoroutine(TextPop(MetalAmounttext));
        }

        if (GoopList.Count > prevGoop)
        {
            prevGoop = GoopList.Count;
            StartCoroutine(TextPop(GoopAmounttext));
        }
    }

    /// <summary>
    ///     Removing redundancy in code above
    /// </summary>
    /// <param name="itemAmount"></param>
    /// <param name="itemText"></param>
    /// <param name="list"></param>
    /// <param name="item"></param>
    private void ItemAdded(int itemAmount, Text itemText, List<ScriptableObjects.Item> list, ScriptableObjects.Item item)
    {
        itemAmount++;
        itemText.text = itemAmount.ToString();
        list.Add(item);
    }

    /// <summary>
    ///     Creates sort of a pop animation for text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator TextPop(Text text)
    {
        var test = true;
        while (test)
        {
            text.fontSize = 50;
            yield return new WaitForSeconds(1);
            text.fontSize = 40;
            test = false;
        }

        yield return null;
    }
}