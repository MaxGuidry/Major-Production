using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFriction : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1000);
	}
	
	// Update is called once per frame
	void Update () {
	    //this.GetComponent<Rigidbody>().AddForce(-Vector3.up * 5);
    }
}
