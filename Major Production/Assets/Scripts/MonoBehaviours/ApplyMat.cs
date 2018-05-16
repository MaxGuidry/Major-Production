using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMat : MonoBehaviour
{
    public Material Material, Visor;
	// Use this for initialization
	void Start () {
	    foreach (var material in GetComponentsInChildren<Renderer>())
	    {
	        material.material = Material;
	        if (material.name.Contains("Visor"))
	        {
	            material.material = Visor;
	        }
	    }
	}
}
