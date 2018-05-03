using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    [HideInInspector]
    public Text GoopAmounttext;
    [HideInInspector]
    public Text MetalAmounttext;
    [HideInInspector]
    public Text StoneAmounttext;
    [HideInInspector]
    public Text WoodAmounttext;
    [HideInInspector]
    public uint woodAmount, stoneAmount, metalAmount, goopAmount;
    private string playerNumber;
    // Use this for initialization
    private void Start()
    {
        switch (gameObject.transform.parent.parent.GetChild(0).tag)
        {
            case "P1":
                playerNumber = "";
                break;
            case "P2":
                playerNumber = " 1";
                break;
            case "P3":
                playerNumber = " 2";
                break;
            case "P4":
                playerNumber = " 3";
                break;
            default:
                break;
        }

        //WoodAmounttext = GameObject.FindGameObjectWithTag("Wood").GetComponentInChildren<Text>();
        //StoneAmounttext = GameObject.FindGameObjectWithTag("Stone").GetComponentInChildren<Text>();
        //MetalAmounttext = GameObject.FindGameObjectWithTag("Metal").GetComponentInChildren<Text>();
        //GoopAmounttext = GameObject.FindGameObjectWithTag("Goop").GetComponentInChildren<Text>();
        foreach (var slot in GetComponentsInChildren<Transform>())
        {
            switch (slot.tag)
            {
                case "Wood":
                    WoodAmounttext = slot.GetComponentInChildren<Text>();
                    break;
                case "Stone":
                    StoneAmounttext = slot.GetComponentInChildren<Text>();
                    break;
                case "Metal":
                    MetalAmounttext = slot.GetComponentInChildren<Text>();
                    break;
                case "Goop":
                    GoopAmounttext = slot.GetComponentInChildren<Text>();
                    break;
                default:
                    break;
            }
        }
    }

    public uint GetNumberItems(ItemType type)
        {
            switch (type)
            {
                case ItemType.None:
                    break;
                case ItemType.Wood:
                    return woodAmount;
                case ItemType.Stone:
                    return stoneAmount;
                case ItemType.Metal:
                    return metalAmount;
                case ItemType.Goop:
                    return goopAmount;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }

            return 0;
        }
    }