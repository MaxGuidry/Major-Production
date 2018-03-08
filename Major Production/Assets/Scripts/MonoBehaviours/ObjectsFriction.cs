using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditorInternal;

public class ObjectsFriction : MonoBehaviour
{
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnCollisionStay(Collision other)
    {
        //add angular velo
        if (!rb)
            return;
        if (rb.velocity.magnitude > 0)
        {
            
            var col = rb.GetComponent<Collider>();
            var pm = col.material;
            var otherpm = other.collider.material;
            if (otherpm.name == "Default (Instance)")
            {
                //Debug.LogError("FUCK");
                return;
            }
            
            

            if (!otherpm)
                return;
            var fric = (pm.dynamicFriction + other.collider.material.dynamicFriction) / 2f;
            //fric *= .5f;
            rb.velocity -= rb.velocity.normalized * (rb.velocity.magnitude * fric);
            if(rb.velocity.magnitude < .1f)
                rb.velocity = Vector3.zero;


        }

        if (rb.angularVelocity.magnitude > 0)
        {
            var col = rb.GetComponent<Collider>();
            var pm = col.material;
            var otherpm = other.collider.material;
            if (!otherpm)
                return;
            var fric = (pm.dynamicFriction + other.collider.material.dynamicFriction) / 2f;
            //fric *= .5f;

            rb.angularVelocity -= rb.angularVelocity.normalized * (rb.angularVelocity.magnitude * fric);
            if(rb.angularVelocity.magnitude < .1f)
                rb.angularVelocity = Vector3.zero;
            
        }

    }
}
