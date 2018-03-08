using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public static Text GoopAmounttext;
    public static Text MetalAmounttext;
    public static Text StoneAmounttext;
    public static Text WoodAmounttext;

    public static int woodAmount, stoneAmount, metalAmount, goopAmount;
    // Use this for initialization
    private void Start()
    {
        WoodAmounttext = GameObject.FindGameObjectWithTag("Wood").GetComponentInChildren<Text>();
        StoneAmounttext = GameObject.FindGameObjectWithTag("Stone").GetComponentInChildren<Text>();
        MetalAmounttext = GameObject.FindGameObjectWithTag("Metal").GetComponentInChildren<Text>();
        GoopAmounttext = GameObject.FindGameObjectWithTag("Goop").GetComponentInChildren<Text>();
    }
}