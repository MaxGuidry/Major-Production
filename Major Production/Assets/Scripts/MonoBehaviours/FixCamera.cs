using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
   private float Sensitivity = 1;

    private CharacterMovement character;

    private Vector3 forward;
    // Use this for initialization
    void Start()
    {
        character = GetComponentInParent<CharacterMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
       
        Quaternion origin = this.transform.rotation;
        Sensitivity = character.Sensitivity;
        var thetaY = Input.GetAxis("Mouse Y") * Mathf.Deg2Rad * Sensitivity * .25f;
        thetaY = ((thetaY > .35f) ? .35f : thetaY);
        thetaY = (thetaY < -.35f ? -.35f : thetaY);
        transform.rotation = new Quaternion(Mathf.Sin(thetaY / 2f) * transform.right.x, Mathf.Sin(thetaY / 2f)
                                                                                     * transform.right.y,
                                 Mathf.Sin(thetaY / 2f) * transform.right.z, Mathf.Cos(thetaY / 2f)) * transform.rotation;

        if (Vector3.Dot(this.transform.up, character.gameObject.transform.up) < .90f)
        {
            this.transform.rotation = origin;
        }
    }
}
