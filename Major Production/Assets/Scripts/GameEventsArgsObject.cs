using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class GameEventArgsObject : GameEventArgs
{
    
    public List<GameEventArgsListenerObject> newListeners = new List<GameEventArgsListenerObject>();
    public void newRaise(params object[] args)
    {
        for (int i = newListeners.Count - 1; i >= 0; i--)
            newListeners[i].NewOnEventRaised(args);
    }

    public void NewRegisterListener(GameEventArgsListenerObject listener)
    {
        if (newListeners.Contains(listener))
            return;
        newListeners.Add(listener);
    }

}
