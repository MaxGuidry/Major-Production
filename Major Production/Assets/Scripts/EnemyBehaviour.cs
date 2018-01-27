
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Range(1,10)]
    public float Speed;
    private GameObject _target;
    public float MinDistance,MaxDistance;
	// Use this for initialization
	void Start () {
		_target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(_target.transform);

	    if (Vector3.Distance(transform.position, _target.transform.position) >= MinDistance)
	        transform.position += transform.forward * Speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, _target.transform.position)<= MaxDistance)
            Debug.Log("Shoot");
    }
}
