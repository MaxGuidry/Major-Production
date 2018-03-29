using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenConnection : MonoBehaviour
{
    public List<GameObject> Panels;
    public SplitScreenPlayers Players;
    public List<string> connectedControllers;
    private Dictionary<GameObject, string> m_Controllers;

    // Use this for initialization
    void Start()
    {
        connectedControllers = Input.GetJoystickNames().ToList();
        Players.PlayersJoined = connectedControllers.Count;
        m_Controllers = new Dictionary<GameObject, string>();
        foreach (var i in Panels)
        {
            i.gameObject.GetComponentInChildren<Text>().text = "Connect Controller";
            m_Controllers.Add(i, "Connect Controller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        connectedControllers = Input.GetJoystickNames().ToList();        
        for (var i = 0; i < connectedControllers.Count; i++)
        {
            var val = "";
            if (connectedControllers[i] != "")
                val = "Player " + (i + 1) + " joined game";
            else
                connectedControllers.RemoveAt(i); 
            m_Controllers[Panels[i]] = val;
        }

        foreach (KeyValuePair<GameObject, string> item in m_Controllers)
        {
            if (item.Value != "")
                item.Key.GetComponentInChildren<Text>().text = item.Value;
            else
                item.Key.GetComponentInChildren<Text>().text = "Connect Controller";
        }
        Players.PlayersJoined = connectedControllers.Count;
    }
}
