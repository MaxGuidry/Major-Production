using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public static Text GoopAmounttext;
    public static Text MetalAmounttext;
    public static Text StoneAmounttext;
    public static Text WoodAmounttext;

    public static uint woodAmount, stoneAmount, metalAmount, goopAmount;

    private CharacterMovement characterMovement;
    private EventSystem eventSystem;
    private GameObject inputEvents;
    // Use this for initialization
    private void Start()
    {
        WoodAmounttext = GameObject.FindGameObjectWithTag("Wood").GetComponentInChildren<Text>();
        StoneAmounttext = GameObject.FindGameObjectWithTag("Stone").GetComponentInChildren<Text>();
        MetalAmounttext = GameObject.FindGameObjectWithTag("Metal").GetComponentInChildren<Text>();
        GoopAmounttext = GameObject.FindGameObjectWithTag("Goop").GetComponentInChildren<Text>();

        characterMovement = FindObjectOfType<CharacterMovement>();
        eventSystem = FindObjectOfType<EventSystem>();
        inputEvents = GameObject.FindGameObjectWithTag("Input");
    }

    public void CycleThroughUI(object[] args)
    {
        if (args.Length < 2)
            return;

        if (args[1] as string != "Submit")
            return;
        if (characterMovement.enabled)
        {
            characterMovement.enabled = false;
            inputEvents.gameObject.SetActive(false);
            eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Wood"));
        }
        else
        {
            characterMovement.enabled = true;
            inputEvents.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(null);
        }
    }

    public static uint GetNumberItems(ItemType type)
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