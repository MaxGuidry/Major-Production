using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPressed : UnityEvent<string> { };
    [System.Serializable]
    public class ButtonHeld : UnityEvent<string> { };
    [System.Serializable]
    public class ButtonReleased : UnityEvent<string> { };

    public ButtonPressed OnButtonPressed;
    public ButtonHeld OnButtonHeld;
    public ButtonReleased OnButtonReleased;

    public List<string> Buttons = new List<string>();

    public Dictionary<string, float> buttonValues = new Dictionary<string, float>();
    // Use this for initialization
    void Start()
    {
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
                OnButtonPressed.Invoke(button);
            else if (value > .1f)
                OnButtonHeld.Invoke(button);
            else if (value < .1f && buttonValues[button] > .1f)
                OnButtonReleased.Invoke(button);
        }
    }

    public void TestInputEvents(string s)
    {
        Debug.Log(s);
    }
}
