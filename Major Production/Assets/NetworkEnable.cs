using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
