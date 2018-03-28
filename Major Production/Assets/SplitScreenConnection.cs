using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            i.gameObject.GetComponentInChildren<Text>().text = "Press A to Join Game";
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
        Players.PlayersJoined = connectedControllers.Count;

        for (var i = 0; i < connectedControllers.Count; i++)
        {
            if (connectedControllers[i] != "")
                connectedControllers[i] += i.ToString();
            else
            {
                Panels[i].gameObject.GetComponentInChildren<Text>().text = "Press A to Join Game";
                connectedControllers.Remove(connectedControllers[i]);
            }

            if (connectedControllers[i].Contains(i.ToString()))
                Panels[i].gameObject.GetComponentInChildren<Text>().text = "Player " + (i + 1) + " joined game";
        }
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
