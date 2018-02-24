using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventArgsListenerObject : GameEventArgsListener
{
    public GameEventArgsObject NewEvent;
    public object NewSender;
    public GameEventArgsResponseObject NewResponse;
    public void NewOnEventRaised(params object[] args)
    {
        if (NewSender == null || NewSender == args[0])
            NewResponse.Invoke(args);
    }

    private void OnEnable()
    {
        if (!NewEvent)
            return;
        NewEvent.RegisterListener(this);
    }
}