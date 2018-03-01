using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour
{



    

   
    public GameEventArgsObject OnButtonPressed;
    public GameEventArgsObject OnButtonHeld; 
    public GameEventArgsObject OnButtonReleased ;

    public List<string> Buttons = new List<string>();

    public Dictionary<string, float> buttonValues = new Dictionary<string, float>();
    
    // Use this for initialization
    void Start()
    {
        buttonValues = new Dictionary<string, float>();
       
        foreach (var button in Buttons)
        {
            buttonValues.Add(button, 0);
        }
        
    }

    void Update()
    {
        foreach (var button in Buttons)
        {
            float value = Input.GetAxis(button);
            if (value > .1f && buttonValues[button] < .1f)
                OnButtonPressed.ObjRaise("Pressed",button);
            else if (value > .1f)
                OnButtonHeld.ObjRaise("Held",button);
            else if (value < .1f && buttonValues[button] > .1f)
                OnButtonReleased.ObjRaise("Released",button);
            buttonValues[button] = value;
        }
    }

    public void TestInputEvents(object[] args)
    {
        Debug.Log(args[0] + ":" + args[1]);
    }
}
