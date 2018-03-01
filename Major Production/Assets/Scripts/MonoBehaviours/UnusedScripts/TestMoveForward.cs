using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveForward : MonoBehaviour {
    public Planet currentPlanet;

    public Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    this.transform.up = (this.transform.position - currentPlanet.center).normalized;
	    this.transform.position += this.transform.forward * .1f;
	    rb.AddForce(((currentPlanet.center - this.transform.position).normalized * currentPlanet.gravity));

    }
}
