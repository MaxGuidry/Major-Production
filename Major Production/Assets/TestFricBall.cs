using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFricBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    this.GetComponent<Rigidbody>().AddForce(Vector3.up * 2000);

    }

    // Update is called once per frame
    void Update () {
		//this.GetComponent<Rigidbody>().AddForce(-this.transform.position.normalized * 10);
	}
}
