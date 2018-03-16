using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public GameObject SelectionObject;
    public static Text GoopAmounttext;
    public static Text MetalAmounttext;
    public static Text StoneAmounttext;
    public static Text WoodAmounttext;

    public static uint woodAmount, stoneAmount, metalAmount, goopAmount;
    public static bool inInventory;
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
        SelectionObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (characterMovement.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) return;
                inInventory = true;
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Wood"));
            }
            else
            {
                inInventory = false;
                characterMovement.enabled = true;
                inputEvents.gameObject.SetActive(true);
                eventSystem.SetSelectedGameObject(null);
            }
        }
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
                SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Wood"));
            }
            else
            {
                SelectionObject.SetActive(false);
                characterMovement.enabled = true;
                inputEvents.gameObject.SetActive(true);
                eventSystem.SetSelectedGameObject(null);
            }
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