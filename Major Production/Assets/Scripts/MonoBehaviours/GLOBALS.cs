using UnityEngine;
using UnityEngine.Events;
using System;
using ScriptableObjects;

[Serializable]
public class GameEventArgsResponse : UnityEvent<UnityEngine.Object[]>{}

public static class GLOBALS
{
    public static bool SplitscreenLocal;
    public static bool SplitscreenOnline;
    public static bool SoloOnline;

}

