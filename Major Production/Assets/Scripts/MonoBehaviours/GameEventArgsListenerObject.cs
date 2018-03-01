using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventArgsListenerObject : GameEventArgsListener
{
    public GameEventArgsObject ObjEvent;
    public object ObjSender;
    public GameEventArgsResponseObject ObjResponse;
    public void ObjOnEventRaised(params object[] args)
    {
        if (ObjSender == null || ObjSender == args[0])
            ObjResponse.Invoke(args);
    }

    private void OnEnable()
    {
        if (!ObjEvent)
            return;
        ObjEvent.ObjRegisterListener(this);
    }
    private void OnDisable()
    {
        ObjEvent.ObjUnregisterListener(this);
    }
}