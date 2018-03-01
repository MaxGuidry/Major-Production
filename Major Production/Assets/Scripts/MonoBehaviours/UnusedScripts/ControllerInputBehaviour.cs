using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputBehaviour : MonoBehaviour {

    public void OnControllerPressedButton(UnityEngine.Object[] args)
    {
        var sender = args[0] as StringVariable;
        if (sender == null)
            return;
        var key = sender.Value;
        if (key == "A")
        {
            Debug.Log("pressed a");
        }
    }
}
