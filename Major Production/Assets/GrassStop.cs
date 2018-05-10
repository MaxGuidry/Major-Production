using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        var rb = this.GetComponent<Rigidbody>();
        if (!rb)
            return;
        if(other.gameObject.transform.root.name.Contains("Planet"))
            Destroy(rb);
        //this.transform.position = other.collider.ClosestPoint(this.transform.position);
        var col = this.GetComponent<Collider>();
        Destroy(col);

    }
}
