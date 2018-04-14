using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public GameObject SelectionObject;
    public GameObject WarpUI;
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
    public bool inInventory;
    private CharacterMovement characterMovement;
    private EventSystem eventSystem;
    private GameObject inputEvents;
    private string playerNumber;
    private string Input;
    public static Coroutine coroutine;
    private int i = 0;
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
        WarpUI.SetActive(false);
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
        Input = "";
        if (characterMovement == null)
            return;
        SelectionObject.SetActive(false);
    }

    public void CycleThroughUI(object[] args)
    {
        switch (gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>().gameObject.transform.tag)
        {
            case "P1":
                Input = "Input";
                break;
            case "P2":
                Input = "Input 1";
                break;
            case "P3":
                Input = "Input 2";
                break;
            case "P4":
                Input = "Input 3";
                break;
        }

        if (inputEvents == null)
            inputEvents = GameObject.FindGameObjectWithTag(Input);
        if (characterMovement == null)
            characterMovement = gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>();
        if (args.Length < 2)
            return;
        string Bbutton = "", SubmitButton = "", Vertical = "", Horizontal = "";
        switch (args[1] as string)
        {
            case "Submit":
                SubmitButton = "Submit";
                if (characterMovement.gameObject.tag != "P1") return;
                break;
            case "Submit1":
                SubmitButton = "Submit1";
                if (characterMovement.gameObject.tag != "P2") return;
                break;
            case "Submit2":
                SubmitButton = "Submit2";
                if (characterMovement.gameObject.tag != "P3") return;
                break;
            case "Submit3":
                SubmitButton = "Submit3";
                if (characterMovement.gameObject.tag != "P4") return;
                break;
            default:
                break;
        }

        switch (args[1] as string)
        {
            case "B":
                Bbutton = "B";
                if (characterMovement.gameObject.tag != "P1") return;
                break;
            case "B1":
                Bbutton = "B1";
                if (characterMovement.gameObject.tag != "P2") return;
                break;
            case "B2":
                Bbutton = "B2";
                if (characterMovement.gameObject.tag != "P3") return;
                break;
            case "B3":
                Bbutton = "B3";
                if (characterMovement.gameObject.tag != "P4") return;
                break;
            default:
                break;
        }

        if (args[1] as string == SubmitButton || args[1] as string == Bbutton)
        {
            if (characterMovement.enabled)
            {
                if (args[1] as string == Bbutton) return;
                SelectionObject.SetActive(true);
                WarpUI.SetActive(true);
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                inInventory = true;
                if (coroutine == null)
                    coroutine = StartCoroutine(NextWarp());

            }
            else
            {
                inInventory = false;
                coroutine = null;
                SelectionObject.SetActive(false);
                characterMovement.enabled = true;
                WarpUI.SetActive(false);
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

    public void WarpPlanet(GameObject planet)
    {
        gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>().SpawnOnOtherPlanet(planet.GetComponent<PlanetBehaviour>());
    }
    
    IEnumerator NextWarp()
    {
        float timer = 0f;


        
            


        while (inInventory)
        {
            var inputs = FindObjectsOfType<InputEvents>();
            InputEvents ie = inputEvents.GetComponent<InputEvents>();
            foreach (var i in inputs)
            {
                if (i.Axis.Contains("DPad Horizontal" + playerNumber))
                {
                   // ie = i;
                    break;
                }
            }

            timer += Time.deltaTime;


            //Debug.Log(ie.prevAxisValues["DPad Horizontal" +playerNumber]);
            if (ie.prevAxisValues["DPad Horizontal" + playerNumber] < -.9f|| ie.prevAxisValues["DPad Horizontal" + playerNumber] > .9f)
            { if (timer < .2f)
                    yield return null;
                else
                {
                    timer = 0;
                }
            }
            if (UnityEngine.Input.GetAxis("DPad Horizontal" + playerNumber) == -1)
            {
                i--;
                if (i > 3)
                    i = 0;
                if (i < 0)
                    i = 3;
                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
            }
            if (UnityEngine.Input.GetAxis("DPad Horizontal" + playerNumber) == 1)
            {
                i++;
                if (i > 3)
                    i = 0;
                if (i < 0)
                    i = 3;
                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
            }
            yield return null;
        }
    }
   
}