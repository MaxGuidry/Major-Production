using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ScriptableObjectEvent : ScriptableObject
{
    [Serializable]
    public class ScriptableCallbackFunc : UnityEvent<ScriptableObject> { }
    
    public ScriptableCallbackFunc CallbackFunc;


}
