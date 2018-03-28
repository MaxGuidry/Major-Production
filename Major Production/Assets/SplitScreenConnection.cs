using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenConnection : MonoBehaviour
{
    public List<GameObject> Panels;
    public SplitScreenPlayers Players;
    public GameEventArgs PlayerJoined;
    //Get Joystick Names
    public List<string> connectedControllers;
    private List<string> oldStrings;
    // Use this for initialization
    void Start()
    {
        connectedControllers = Input.GetJoystickNames().ToList();
        oldStrings = connectedControllers;
        Players.PlayersJoined = connectedControllers.Count;
        foreach (var i in Panels)
            i.gameObject.GetComponentInChildren<Text>().text = "Connect Controller";
    }

    // Update is called once per frame
    void Update()
    {
        connectedControllers = Input.GetJoystickNames().ToList();
        if (oldStrings.Count != connectedControllers.Count)
        {
            PlayerJoined.Raise(this);
        }
        oldStrings = connectedControllers;
        for (var i = 0; i < connectedControllers.Count; i++)
        {
            if (connectedControllers[i] == "")
            {
                Panels[i].gameObject.GetComponentInChildren<Text>().text = "Connect Controller";
                connectedControllers.Remove(connectedControllers[i]);
                i -=1;
            }

            if (connectedControllers[i].Contains("Controller"))
            {
                connectedControllers[i] += i.ToString();
            }

            if (connectedControllers[i].Contains(i.ToString()))
            {
                Panels[i].gameObject.GetComponentInChildren<Text>().text = "Player " + (i + 1) + " joined game";
            }
        }
        Players.PlayersJoined = connectedControllers.Count;
    }

    public void PlayerJoinedRaise()
    {
        if (connectedControllers.Count > 0)
        {
            //Iterate over every element
            for (var i = 0; i < connectedControllers.Count; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(connectedControllers[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + connectedControllers[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");
                }
            }
        }
    }
}
