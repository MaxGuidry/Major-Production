using System;
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

    // Use this for initialization
    private void Start()
    {
        Inventory = FindObjectOfType<InventoryBehaviour>();
        WoodAmount = 0;
        StoneAmount = 0;
        MetalAmount = 0;
        GoopAmount = 0;
        WoodAmounttext.text = 0.ToString();
        StoneAmounttext.text = 0.ToString();
        MetalAmounttext.text = 0.ToString();
        GoopAmounttext.text = 0.ToString();
    }
    /// <summary>
    /// Checks each item in inventory then makes a number
    ///  that is equal to the amount in inventory
    /// </summary>
    public void CheckInv()
    {
        WoodAmount = 0;
        StoneAmount = 0;
        MetalAmount = 0;
        GoopAmount = 0;
        foreach (var item in Inventory.ActiveInventory)
            switch (item.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Wood:
                    WoodAmount++;
                    WoodAmounttext.text = WoodAmount.ToString();
                    StartCoroutine(TextPop(WoodAmounttext));
                    break;
                case ItemType.Stone:
                    StoneAmount++;
                    StoneAmounttext.text = StoneAmount.ToString();
                    StartCoroutine(TextPop(StoneAmounttext));
                    break;
                case ItemType.Metal:
                    MetalAmount++;
                    MetalAmounttext.text = MetalAmount.ToString();
                    StartCoroutine(TextPop(MetalAmounttext));
                    break;
                case ItemType.Goop:
                    GoopAmount++;
                    GoopAmounttext.text = GoopAmount.ToString();
                    StartCoroutine(TextPop(GoopAmounttext));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
    /// <summary>
    /// Creates sort of a pop animation for text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static IEnumerator TextPop(Text text)
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