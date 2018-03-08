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

        if (rb.velocity.magnitude > 0)
        {

            var col = rb.GetComponent<Collider>();
            var pm = col.material;
            var otherpm = other.collider.material;
            if (!otherpm)
                return;
            var fric = (pm.dynamicFriction + other.collider.material.dynamicFriction) / 2f;

            rb.velocity -= rb.velocity.normalized * fric;


        }

    }
}
