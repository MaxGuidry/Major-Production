using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEventArgsObject")]
public class GameEventArgsObject : GameEventArgs
{
    
    public List<GameEventArgsListenerObject> objListeners = new List<GameEventArgsListenerObject>();
    public void ObjRaise(params object[] args)
    {
        for (int i = objListeners.Count - 1; i >= 0; i--)
            objListeners[i].ObjOnEventRaised(args);
    }

    public void ObjRegisterListener(GameEventArgsListenerObject listener)
    {
        if (objListeners.Contains(listener))
            return;
        objListeners.Add(listener);
    }
    public void ObjUnregisterListener(GameEventArgsListenerObject listener)
    {
        if (!objListeners.Contains(listener))
        {
            Debug.LogError("listener is not in list");
            return;
        }

        objListeners.Remove(listener);
    }
}
