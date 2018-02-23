using System;
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

    // Update is called once per frame
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
                    break;
                case ItemType.Stone:
                    StoneAmount++;
                    StoneAmounttext.text = StoneAmount.ToString();
                    break;
                case ItemType.Metal:
                    MetalAmount++;
                    MetalAmounttext.text = MetalAmount.ToString();
                    break;
                case ItemType.Goop:
                    GoopAmount++;
                    GoopAmounttext.text = GoopAmount.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}