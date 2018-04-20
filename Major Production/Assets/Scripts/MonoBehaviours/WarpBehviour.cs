using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarpBehviour : MonoBehaviour
{

    public GameObject SelectionObject;
    public GameObject WarpUI;
    public Coroutine coroutine;
    public bool warping;
    private CharacterMovement characterMovement;
    private EventSystem eventSystem;
    [HideInInspector]
    public GameObject inputEvents;
    private bool inInventory;
    private string playerNumber;
    private string playerTag;
    private string InputString;
    private string EventString;

    private int i;

    // Use this for initialization
    void Start()
    {
        foreach (var child in gameObject.GetComponentsInChildren<Image>())
        {
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
    void Update()
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
            if (Input.GetAxis("Submit" + playerNumber) > .9f)
            {
                if (inInventory)
                {
                    SelectionObject.SetActive(false);
                    characterMovement.enabled = true;
                    WarpUI.SetActive(false);
                    inputEvents.gameObject.SetActive(true);
                    eventSystem.SetSelectedGameObject(null);
                    if (Input.GetAxis("Submit" + playerNumber) > .9f)
                    {
                        inInventory = false;
                    }
                }
                else
                {
                    if (Input.GetAxis("Submit" + playerNumber) > .9f)
                    {
                        inInventory = true;
                    }
                    SelectionObject.SetActive(true);
                    WarpUI.SetActive(true);
                    characterMovement.enabled = false;
                    inputEvents.gameObject.SetActive(false);
                    eventSystem.SetSelectedGameObject(WarpUI.transform.GetChild(i).gameObject);
                    SelectionObject.transform.position = eventSystem.currentSelectedGameObject.transform.position;
                }
            }
        }

        if (inInventory)
        {
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
    }

    public void WarpPlanet(GameObject planet)
    {
        var delay = 2f;
        if (coroutine == null)
        {
            WarpUI.SetActive(false);
            SelectionObject.SetActive(false);
            warping = true;
            coroutine = characterMovement.StartCoroutine(characterMovement.SpawnDelay(planet.GetComponent<PlanetBehaviour>(), delay, this));
        }
    }
}
