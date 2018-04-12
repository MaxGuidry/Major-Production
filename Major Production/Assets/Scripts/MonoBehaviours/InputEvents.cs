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

    public Dictionary<string, float> buttonValues;

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < 100; i++)
        //    text.text += '\n';
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
            if (value > .01f && buttonValues[button] < .01f)
                OnButtonPressed.ObjRaise("Pressed", button);
            else if (value > .01f)
                OnButtonHeld.ObjRaise("Held", button);
            else if (value < .01f && buttonValues[button] > .01f)
                OnButtonReleased.ObjRaise("Released", button);
            buttonValues[button] = value;
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