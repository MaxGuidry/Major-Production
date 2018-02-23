using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    private InventoryBehaviour Inventory;
    public Text WoodAmounttext;
    public Text StoneAmounttext;
    public Text MetalAmounttext;
    public Text GoopAmounttext;

    private int WoodAmount;
    private int StoneAmount;
    private int MetalAmount;
    private int GoopAmount;

    // Use this for initialization
	void Start ()
	{
	    Inventory = FindObjectOfType<InventoryBehaviour>();
	    WoodAmount = 0;
	    StoneAmount = 0;
	    MetalAmount = 0;
	    GoopAmount = 0;
	}
	
	// Update is called once per frame
	public void CheckInv ()
	{
	    foreach (var item in Inventory.ActiveInventory)
	    {
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
}
