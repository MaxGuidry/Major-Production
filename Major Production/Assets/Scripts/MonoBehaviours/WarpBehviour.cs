﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarpBehviour : MonoBehaviour
{
    private CharacterMovement characterMovement;
    public Coroutine coroutine;
    private string EventString;
    [SerializeField] private EventSystem eventSystem;
    private int i;
    public GameObject inputEvents;
    private string InputString;
    private string playerNumber;
    private string playerTag;
    public GameObject SelectionObject;
    private bool test;
    public GameObject UIInputevents;
    public bool warping;
    public GameObject WarpUI;
    private float Timer = 3f;
    public Text InstrucText, WarpText;
    public GameObject WarpEffect;
    private bool TakingOff, Landing;
    // Use this for initialization
    private void Start()
    {
        WarpText.gameObject.SetActive(false);
        foreach (var child in gameObject.GetComponentsInChildren<Image>())
            switch (child.tag)
            {
                case "WarpUI":
                    WarpUI = child.gameObject;
                    WarpUI.SetActive(false);
                    break;
                case "Select":
                    SelectionObject = child.gameObject;
                    SelectionObject.SetActive(false);
                    break;
                default:
                    break;
            }
        InputString = "";
        EventString = "";
        characterMovement = gameObject.transform.parent.GetComponentInChildren<CharacterMovement>();
        playerTag = gameObject.transform.parent.GetChild(0).tag;
        switch (playerTag)
        {
            case "P1":
                playerNumber = "";
                break;
            case "P2":
                playerNumber = "1";
                break;
            case "P3":
                playerNumber = "2";
                break;
            case "P4":
                playerNumber = "3";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
       
        playerTag = gameObject.transform.parent.GetComponentInChildren<PlayerStatBehaviour>().tag;
        if (characterMovement == null)
            characterMovement = gameObject.transform.parent.GetComponentInChildren<CharacterMovement>();
        switch (playerTag)
        {
            case "P1":
                InputString = characterMovement == null ? null : "Input";
                EventString = "Event";
                break;
            case "P2":
                InputString = characterMovement == null ? null : "Input 1";
                EventString = "Event 1";
                break;
            case "P3":
                InputString = characterMovement == null ? null : "Input 2";
                EventString = "Event 2";
                break;
            case "P4":
                InputString = characterMovement == null ? null : "Input 3";
                EventString = "Event 3";
                break;
            default:
                break;
        }

        if (inputEvents == null)
            inputEvents = GameObject.FindGameObjectWithTag(InputString);
        eventSystem = GameObject.FindGameObjectWithTag(EventString).GetComponent<EventSystem>();

      

        if (!warping)
        {
            if (eventSystem != null)
            {
                if (eventSystem.currentSelectedGameObject != null)
                {
                    if (eventSystem.currentSelectedGameObject.transform.parent != null)
                    {
                        if (eventSystem.currentSelectedGameObject.transform.parent.parent != null)
                        {
                            if (eventSystem.currentSelectedGameObject.transform.parent.parent.name == "ActionBar")
                                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(2).gameObject);
                        }
                    }
                }

            }
            WarpText.text = "";
            WarpText.gameObject.SetActive(false);
            if (WarpUI.gameObject.activeInHierarchy)
            {
                foreach (var child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if (child.tag == "Actions")
                    {
                        child.GetComponent<Image>().enabled = false;
                        foreach (var image in child.GetComponentsInChildren<Image>())
                        {
                            image.enabled = false;
                        }
                    }
                }
                InstrucText.text = "Close Warp Menu";
                UIInputevents.GetComponent<GameEventArgsListenerObject>().enabled = true;
                UIInputevents.gameObject.SetActive(true);
                UIInputevents.GetComponent<InputEvents>().enabled = true;
                characterMovement.enabled = false;
                inputEvents.gameObject.SetActive(false);
                if (Input.GetAxis("A" + playerNumber) >= .9f)
                {

                    if (eventSystem == null)
                    {
                        Debug.LogError("Event System is null");
                        return;
                    }

                    if (eventSystem.currentSelectedGameObject == null)
                    {
                        Debug.LogError("Current Selected Game Object is null");
                        return;
                    }
                    if (eventSystem.currentSelectedGameObject.GetComponent<Button>() == null)
                    {
                        Debug.LogError("Button is null");
                        Debug.LogError(eventSystem.currentSelectedGameObject.name + " ,"+ eventSystem.currentSelectedGameObject.transform.parent.name);
                        return;
                    }
                    if (eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick == null)
                    {
                        Debug.LogError("Button On Click is null");
                        return;
                    }
                    if (eventSystem.currentSelectedGameObject.GetComponent<Button>().enabled)
                        eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                }

                if (Input.GetAxis("Horizontal" + playerNumber) >= .5f)
                {
                    if (i == 0)
                        i = 1;
                    if (i == 2)
                        i = 3;
                    eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                    SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                }

                if (Input.GetAxis("Horizontal" + playerNumber) <= -.5f)
                {
                    if (i == 1)
                        i = 0;
                    if (i == 3)
                        i = 2;
                    eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                    SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                }

                if (Input.GetAxis("Vertical" + playerNumber) >= .5f)
                {
                    switch (i)
                    {
                        case 0:
                            return;
                        case 1:
                            return;
                        case 2:
                            i = 0;
                            break;
                        case 3:
                            i = 1;
                            break;
                        default:
                            break;
                    }

                    eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                    SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                }

                if (Input.GetAxis("Vertical" + playerNumber) <= -.5f)
                {
                    switch (i)
                    {
                        case 0:
                            i = 2;
                            break;
                        case 1:
                            i = 3;
                            break;
                        case 2:
                            return;
                        case 3:
                            return;
                        default:
                            break;
                    }

                    eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                    SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                }
            }
            else
            {
                foreach (var child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if (child.tag == "Actions")
                    {
                        child.GetComponent<Image>().enabled = true;
                        foreach (var image in child.GetComponentsInChildren<Image>())
                        {
                            image.enabled = true;
                        }
                    }
                }
                InstrucText.text = "Open Warp Menu";
                UIInputevents.GetComponent<GameEventArgsListenerObject>().enabled = false;
                UIInputevents.gameObject.SetActive(false);
                inputEvents.gameObject.SetActive(true);
                UIInputevents.GetComponent<InputEvents>().enabled = false;
                characterMovement.enabled = true;
            }
        }
        else
        {
            WarpText.gameObject.SetActive(true);
            WarpText.GetComponent<TweenScaleBehaviour>().TweenScale();
            WarpText.GetComponent<TweenScaleBehaviour>().isRunning = false;

            Timer -= Time.deltaTime;
            if (Timer <= 1f)
            {
                Timer = 3;
            }
        }
    }

    public void WarpPlanet(GameObject planet)
    {
        if (WarpUI.activeInHierarchy)
        {
            var delay = 2f;
            if (coroutine == null)
            {
                WarpUI.SetActive(false);
                SelectionObject.SetActive(false);
                foreach (var child in WarpUI.GetComponentsInChildren<Button>())
                {
                    child.enabled = false;
                    child.interactable = false;
                }
                warping = true;
                StartCoroutine(SpawnEffect());
                coroutine = characterMovement.StartCoroutine(
                    characterMovement.SpawnDelay(planet.GetComponent<PlanetBehaviour>(), delay, this, WarpText, Timer));
                test = false;
            }
        }
    }

    public void CycleUI(object[] args)
    {
        if (args.Length > 1)
            if (args[1] as string == "Submit" + playerNumber)
                if (!warping)
                    if (WarpUI.gameObject.activeInHierarchy)
                    {
                        if (test)
                            ToggleUI(false);
                        else if (!test)
                            test = true;
                    }
                    else
                    {
                        ToggleUI(true);
                    }
    }

    private void ToggleUI(bool toogle)
    {
        if (toogle)
        {
            foreach (var child in WarpUI.GetComponentsInChildren<Button>())
            {
                child.enabled = true;
                child.interactable = true;
            }

            UIInputevents.GetComponent<InputEvents>().enabled = true;
            UIInputevents.gameObject.SetActive(true);
            SelectionObject.SetActive(true);
            WarpUI.SetActive(true);
            characterMovement.enabled = false;
            inputEvents.gameObject.SetActive(false);

            foreach (var planetBehaviour in FindObjectsOfType<PlanetBehaviour>())
            {
                foreach (var rb in planetBehaviour.rbs)
                {
                    var tag = rb.gameObject.transform.tag;
                    if (tag == playerTag)
                    {
                        switch (planetBehaviour.gameObject.name)
                        {
                            case "Planet1":
                                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(0).gameObject);
                                WarpUI.transform.GetChild(0).GetComponent<Button>().enabled = false;
                                break;
                            case "Planet2":
                                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(1).gameObject);
                                WarpUI.transform.GetChild(1).GetComponent<Button>().enabled = false;
                                break;
                            case "Planet3":
                                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(2).gameObject);
                                WarpUI.transform.GetChild(2).GetComponent<Button>().enabled = false;
                                break;
                            case "Planet4":
                                eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(3).gameObject);
                                WarpUI.transform.GetChild(3).GetComponent<Button>().enabled = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
        }
        else if (!toogle)
        {
            foreach (var child in WarpUI.GetComponentsInChildren<Button>())
            {
                child.enabled = false;
                child.interactable = false;
            }

            UIInputevents.GetComponent<InputEvents>().enabled = false;
            UIInputevents.gameObject.SetActive(false);
            SelectionObject.SetActive(false);
            characterMovement.enabled = true;
            WarpUI.SetActive(false);
            inputEvents.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(null);
        }
    }

    private IEnumerator SpawnEffect()
    {
        var done = false;
        while (!done)
        {
            var effect = Instantiate(WarpEffect, Vector3.zero, Quaternion.identity);
            effect.gameObject.transform.SetParent(characterMovement.gameObject.transform);
            effect.transform.localEulerAngles = new Vector3(-90, 0, 0);
            effect.transform.localScale *= 3;
            effect.transform.localPosition = Vector3.zero;
            done = true;
            yield return new WaitForSeconds(4);
            Destroy(effect);
        }
    }
}