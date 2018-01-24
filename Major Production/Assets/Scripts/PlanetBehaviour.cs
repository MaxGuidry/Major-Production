using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    public List<Rigidbody> rbs = new List<Rigidbody>();
    public Planet planet;
	// Use this for initialization
    void Awake()
    {
        planet.Initialize(this.transform.position, this.transform.localScale.x / 2f);
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.transform.position = planet.center;
        //g.transform.localScale = new Vector3(.1f,.1f,.1f);
    }
	void Start ()
	{
	    rbs = GameObject.FindObjectsOfType<Rigidbody>().ToList();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    foreach (var rb in rbs)
	    {
	         
	    }
	}

    void Attract(Rigidbody rb)
    {
        rb.AddForce(((planet.center - this.transform.position).normalized * planet.gravity));

    }
}
