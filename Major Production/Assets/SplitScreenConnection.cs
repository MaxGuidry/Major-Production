using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenConnection : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Panel;
    public SplitScreenPlayers Players;
    public GameEventArgs PlayerJoined;
    //Get Joystick Names
    public List<string> connectedControllers;
    private List<string> oldStrings;
    private GameObject temp;
    private int X;
    // Use this for initialization
    void Start()
    {

        X = 0;
        connectedControllers = Input.GetJoystickNames().ToList();
        oldStrings = connectedControllers;
        Players.PlayersJoined = connectedControllers.Count;
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
                connectedControllers.Remove(connectedControllers[i]);
                i = -1;
                Destroy(temp);
            }
        }
        Players.PlayersJoined = connectedControllers.Count;
    }

    public void PlayerJoinedRaise()
    {
        X += 10;
        if (connectedControllers.Count > 0)
        {
            for (int i = 0; i < Players.PlayersJoined; i++)
            {
                temp = Instantiate(Prefab, new Vector3(Panel.transform.position.x + X,
                        Panel.transform.position.y, Panel.transform.position.z),
                    Quaternion.identity);
                temp.transform.parent = Panel.transform;
            }
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
