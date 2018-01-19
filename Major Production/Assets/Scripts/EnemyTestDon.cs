using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestDon : MonoBehaviour, IDamageable
{
    public GameObject target;

    public float speed;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
	    var step = speed * Time.deltaTime;
	    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
