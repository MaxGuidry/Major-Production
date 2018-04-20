using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class InputEvents : MonoBehaviour
{




    // public Text text;

    public GameEventArgsObject OnButtonPressed;
    public GameEventArgsObject OnButtonHeld;
    public GameEventArgsObject OnButtonReleased;
    public List<string> Buttons = new List<string>();
    public List<string> Axis = new List<string>();
    public Dictionary<string, float> buttonValues;
    [HideInInspector] public Dictionary<string, float> prevButtonValues;

    public Dictionary<string, float> axisValues;
    [HideInInspector] public Dictionary<string, float> prevAxisValues;


    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < 100; i++)
        //    text.text += '\n';
        buttonValues = new Dictionary<string, float>();
        prevButtonValues = new Dictionary<string, float>();
        axisValues = new Dictionary<string, float>();
        prevAxisValues = new Dictionary<string, float>();
        foreach (var button in Buttons)
        {
            prevButtonValues.Add(button, 0);
            buttonValues.Add(button, 0);
        }

        foreach (var axis in Axis)
        {
            axisValues.Add(axis, 0);
            prevAxisValues.Add(axis, 0);
        }
    }

    void Update()
    {
        foreach (var button in Buttons)
        {
            prevButtonValues[button] = buttonValues[button];
            float value = Input.GetAxis(button);
            if (value > .001f && buttonValues[button] < .001f)
                OnButtonPressed.ObjRaise("Pressed", button);
            else if (value > .001f)
                OnButtonHeld.ObjRaise("Held", button);
            else if (value < .001f && buttonValues[button] > .001f)
                OnButtonReleased.ObjRaise("Released", button);
            buttonValues[button] = value;
        }

        foreach (var axis in Axis)
        {

            prevAxisValues[axis] = axisValues[axis];
            float value = Input.GetAxis(axis);
            if (value > .001f && axisValues[axis] < .001f)
                OnButtonPressed.ObjRaise("Pressed", axis);
            else if (value > .001f)
                OnButtonHeld.ObjRaise("Held", axis);
            else if (value < .001f && axisValues[axis] > .001f)
                OnButtonReleased.ObjRaise("Released", axis);
            axisValues[axis] = value;
        }
    }

    //public void TestInputEvents(object[] args)
    //{
    //    //GameObject Text = new GameObject();
    //    //Text.AddComponent<RectTransform>();
    //    //Text.AddComponent<Text>();
    //    //Text.transform.SetParent(panel.transform, false);
    //    //text.text += "\n" + (args[0] + ":" + args[1]);
    //    int newlines = 0;
    //    foreach (var c in text.text)
    //    {
    //        if (c == '\n')
    //            newlines++;
    //    }
    //    if (newlines > 100)
    //    {
    //        int count = 0;
    //        char c = '\0';
    //        while (c != '\n')
    //        {
    //            c = text.text[count];
    //            count++;
    //        }

    //        text.text = text.text.Remove(0, count);
    //    }
    //    //Text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
    //}
}