using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<PlayerStatBehaviour>();
        if (character == null)
            return;
        character.TakeDamage(900000000);
    }
}
