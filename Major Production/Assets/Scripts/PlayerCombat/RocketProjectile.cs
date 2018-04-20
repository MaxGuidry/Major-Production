using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{

    public float rocketSpeed;
    public float upSpeed;
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    StartCoroutine(DestroyTime());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	    //this.transform.position += this.transform.up *(upSpeed /100f);
	    //this.transform.position += this.transform.forward *(rocketSpeed/100f);
        if(rb.velocity.magnitude < 17)
            rb.AddForce(transform.forward * (rocketSpeed / 10f), ForceMode.Impulse);
        else
        {
            rb.velocity = rb.velocity.normalized * 17f;
        }
        rb.AddForce(transform.up *( upSpeed/100f),ForceMode.Impulse);


    }

    void OnCollisionEnter(Collision other)
    {
        //explosion
        Collider[] knockbacks = Physics.OverlapSphere(this.transform.position, 8);
        for (int i = 0; i < knockbacks.Length; i++)
        {
            var rb = knockbacks[i].gameObject.GetComponent<Rigidbody>();
            if (!rb)
                continue;
            if(rb == this.rb)
                continue;
            rb.AddExplosionForce(1005,this.transform.position,8);
        }
        Destroy(this.gameObject);
    }
    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);

    }
}
