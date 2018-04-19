﻿using System;
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
    public CharacterMovement characterMovement;
    private EventSystem eventSystem;
    public GameObject inputEvents;
    private string playerNumber;
    private string Input;
    public Coroutine coroutine;
    private int i = 0;

    public bool warping = false;
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

    private void Update()
    {
        if (inInventory)
            SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
    }
    public void CycleThroughUI(object[] args)
    {

        var playerTag = gameObject.transform.parent.parent.GetComponentInChildren<PlayerStatBehaviour>().tag;
        if (characterMovement == null)
            characterMovement = gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>();
        switch (playerTag)
        {
            case "P1":
                Input = characterMovement == null ? null : "Input";
                break;
            case "P2":
                Input = characterMovement == null ? null : "Input 1";
                break;
            case "P3":
                Input = characterMovement == null ? null : "Input 2";
                break;
            case "P4":
                Input = characterMovement == null ? null : "Input 3";
                break;
            default:
                break;
        }

        if (inputEvents == null)
            inputEvents = GameObject.FindGameObjectWithTag(Input);
        //if (characterMovement == null)
        //    characterMovement = gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>();
        if (args.Length < 2)
            return;
        string Bbutton = "", SubmitButton = "", Vertical = "", Horizontal = "";
        switch (args[1] as string)
        {
            case "Submit":
                SubmitButton = "Submit";
                if (playerTag != "P1") return;
                break;
            case "Submit1":
                SubmitButton = "Submit1";
                if (playerTag != "P2") return;
                break;
            case "Submit2":
                SubmitButton = "Submit2";
                if (playerTag != "P3") return;
                break;
            case "Submit3":
                SubmitButton = "Submit3";
                if (playerTag != "P4") return;
                break;
            default:
                break;
        }

        switch (args[1] as string)
        {
            case "B":
                Bbutton = "B";
                if (playerTag != "P1") return;
                break;
            case "B1":
                Bbutton = "B1";
                if (playerTag != "P2") return;
                break;
            case "B2":
                Bbutton = "B2";
                if (playerTag != "P3") return;
                break;
            case "B3":
                Bbutton = "B3";
                if (playerTag != "P4") return;
                break;
            default:
                break;
        }
        if (warping)
            return;
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
                //if (coroutine == null)
                //    coroutine = StartCoroutine(NextWarp());

            }
            else
            {
                inInventory = false;
                //coroutine = null;
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
        var delay = 2f;

        var charMov = gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>();
        if (coroutine == null)
        {
            WarpUI.SetActive(false);
            SelectionObject.SetActive(false);
            warping = true;
            coroutine = charMov.StartCoroutine(charMov.SpawnDelay(planet.GetComponent<PlanetBehaviour>(), delay, this));
        }
        //gameObject.transform.parent.parent.GetComponentInChildren<CharacterMovement>().SpawnOnOtherPlanet(planet.GetComponent<PlanetBehaviour>());
    }

    IEnumerator NextWarp()
    {
        //float timer = 0f;
        while (inInventory)
        {

            #region Max
            //var inputs = FindObjectsOfType<InputEvents>();
            //InputEvents ie = inputEvents.GetComponent<InputEvents>();
            //foreach (var i in inputs)
            //{
            //    if (i.Axis.Contains("DPad Horizontal" + playerNumber))
            //    {
            //        // ie = i;
            //        break;
            //    }
            //}
            //timer += Time.deltaTime;
            ////Debug.Log(ie.prevAxisValues["DPad Horizontal" +playerNumber]);
            //if (ie.prevAxisValues["DPad Horizontal" + playerNumber] < -.9f || ie.prevAxisValues["DPad Horizontal" + playerNumber] > .9f)
            //{
            //    if (timer < .2f)
            //        yield return null;
            //    else
            //    {
            //        timer = 0;
            //    }
            //}
            #endregion

            if (UnityEngine.Input.GetAxis("DPad Horizontal" + playerNumber) == -1)
            {
                i--;
                CyclePlanet();
            }
            if (UnityEngine.Input.GetAxis("DPad Horizontal" + playerNumber) == 1)
            {
                i++;
                CyclePlanet();
            }
            yield return new WaitForSeconds(.15f);
        }
    }

    private void CyclePlanet()
    {
        if (i > WarpUI.transform.childCount - 1)
            i = 0;
        if (i < 0)
            i = WarpUI.transform.childCount - 1;

        eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
        SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
    }
}