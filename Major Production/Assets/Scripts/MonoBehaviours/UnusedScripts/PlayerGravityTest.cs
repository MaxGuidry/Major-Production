using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerGravityTest : MonoBehaviour
{
    public PlanetTest planet;

    private Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
	{
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetTest>();
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void Update ()
	{
        // Allow this body to be influenced by planet's gravity
        if (planet)
	        planet.Attract(rigidbody);
	}
}
