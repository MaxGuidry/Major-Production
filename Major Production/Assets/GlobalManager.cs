using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GlobalManager : MonoBehaviour {

    void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            GLOBALS.SplitscreenOnline = false;
        GLOBALS.SplitscreenLocal = false;
        GLOBALS.SoloOnline = true;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
