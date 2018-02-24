using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour
{



    public void TestListener(object[] args)
    {
        Debug.Log(args[0]);
    }

    public GameEventArgsListenerObject PressedListener;
    public GameEventArgsListenerObject HeldListener; 
    public GameEventArgsListenerObject ReleasedListener;
    private GameEventArgsObject OnButtonPressed;
    private GameEventArgsObject OnButtonHeld; 
    private GameEventArgsObject OnButtonReleased ;

    public List<string> Buttons = new List<string>();

    public Dictionary<string, float> buttonValues = new Dictionary<string, float>();
    
    // Use this for initialization
    void Start()
    {
        buttonValues = new Dictionary<string, float>();
        PressedListener = this.gameObject.AddComponent<GameEventArgsListenerObject>();
        HeldListener = this.gameObject.AddComponent<GameEventArgsListenerObject>();
        ReleasedListener = this.gameObject.AddComponent<GameEventArgsListenerObject>();
        OnButtonPressed = ScriptableObject.CreateInstance<GameEventArgsObject>();
        OnButtonHeld = ScriptableObject.CreateInstance<GameEventArgsObject>();
        OnButtonReleased = ScriptableObject.CreateInstance<GameEventArgsObject>();
        foreach (var button in Buttons)
        {
            buttonValues.Add(button, 0);
        }
        PressedListener.NewEvent = OnButtonPressed;
        PressedListener.NewEvent.NewRegisterListener(PressedListener);
        HeldListener.NewEvent = OnButtonHeld;
        ReleasedListener.NewEvent = OnButtonReleased;
        PressedListener.NewResponse = new GameEventArgsResponseObject();
        PressedListener.NewResponse.AddListener(TestInputEvents);
    }

    void Update()
    {
        foreach (var button in Buttons)
        {
            float value = Input.GetAxis(button);
            if (value > .1f && buttonValues[button] < .1f)
                OnButtonPressed.newRaise(button);
            else if (value > .1f)
                OnButtonHeld.newRaise(button);
            else if (value < .1f && buttonValues[button] > .1f)
                OnButtonReleased.newRaise(button);
        }
    }

    public void TestInputEvents(object[] args)
    {
        Debug.Log(args[0]);
    }
}
