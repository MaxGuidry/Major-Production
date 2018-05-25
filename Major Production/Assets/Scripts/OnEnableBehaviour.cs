using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableBehaviour : MonoBehaviour
{

    public UnityEngine.Events.UnityEvent OnEnableResponse; 

    void OnEnable()
    {
        OnEnableResponse.Invoke();
        
    }
}
