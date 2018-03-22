using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public GameObject SelectionObject;
    public Text GoopAmounttext;
    public Text MetalAmounttext;
    public Text StoneAmounttext;
    public Text WoodAmounttext;

    public uint woodAmount, stoneAmount, metalAmount, goopAmount;
    public bool inInventory;
    private CharacterMovement characterMovement;
    private EventSystem eventSystem;
    private GameObject inputEvents;
    // Use this for initialization
    private void Start()
    {
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

        characterMovement = gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>();
        eventSystem = gameObject.transform.parent.parent.GetComponentInChildren<EventSystem>();
        inputEvents = GameObject.FindGameObjectWithTag("Input");
        SelectionObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (characterMovement.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) return;
                SelectionObject.SetActive(true);
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                eventSystem.SetSelectedGameObject(gameObject.transform.GetChild(0).gameObject);
                inInventory = true;
            }
            else
            {
                inInventory = false;
                SelectionObject.SetActive(false);
                characterMovement.enabled = true;
                inputEvents.gameObject.SetActive(true);
                eventSystem.SetSelectedGameObject(null);
            }
        }

        if (inInventory)
            SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
    }

    public void CycleThroughUI(object[] args)
    {
        if (args.Length < 2)
            return;

        if (args[1] as string == "Submit" || args[1] as string == "B")
        {
            if (characterMovement.enabled)
            {
                if (args[1] as string == "B") return;
                SelectionObject.SetActive(true);
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                eventSystem.SetSelectedGameObject(gameObject.transform.GetChild(0).gameObject);
                inInventory = true;
            }
            else
            {
                inInventory = false;
                SelectionObject.SetActive(false);
                characterMovement.enabled = true;
                inputEvents.gameObject.SetActive(true);
                eventSystem.SetSelectedGameObject(null);
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