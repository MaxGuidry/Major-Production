using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{

    public Planet planet;
	// Use this for initialization
    void Awake()
    {
        planet.Initialize(this.transform.position, this.transform.localScale.x / 2f);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
