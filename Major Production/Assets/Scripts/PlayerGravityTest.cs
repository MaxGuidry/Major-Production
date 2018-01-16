using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityTest : MonoBehaviour
{
    public PlanetTest planet;

    private Transform playerTransform;
	// Use this for initialization
	void Start ()
	{
	    GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

	    playerTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (planet)
	        planet.Attract(playerTransform);
	}
}
