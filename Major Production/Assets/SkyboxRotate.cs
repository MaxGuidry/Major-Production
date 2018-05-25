using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class SkyboxRotate : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    RenderSettings.skybox.SetFloat("_Rotation", Time.time * 5f);
	}
}
