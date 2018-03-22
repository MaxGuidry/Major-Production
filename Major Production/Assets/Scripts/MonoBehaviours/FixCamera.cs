using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

public class FixCamera : NetworkBehaviour
{
    private float Sensitivity = 1;

    private CharacterMovement character;

    public Transform follow, pivotX, pivotY;
    private List<CharacterMovement> cms = new List<CharacterMovement>();
    //private float lastDot;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(GetCharacter());

        //lastDot = Vector3.Dot(pivotX.up, follow.up);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (!follow)
            return;

        this.transform.position = follow.transform.position;
        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, follow.transform.up) * this.transform.rotation;// follow.transform.rotation;
                                                                                                                              // this.transform.rotation = Quaternion.FromToRotation(this.transform.right, follow.transform.right) * this.transform.rotation;// follow.transform.rotation;
        Quaternion origin = pivotX.rotation;
        //Sensitivity = character.Sensitivity;
        var thetaY = Input.GetAxis("Mouse Y") * Mathf.Deg2Rad * Sensitivity * .5f;
        //thetaY = ((thetaY > .35f) ? .35f : thetaY);
        //thetaY = (thetaY < -.35f ? -.35f : thetaY);
        pivotX.rotation = new Quaternion(Mathf.Sin(thetaY / 2f) * pivotX.right.x, Mathf.Sin(thetaY / 2f)
                                                                                     * pivotX.right.y,
                                 Mathf.Sin(thetaY / 2f) * pivotX.right.z, Mathf.Cos(thetaY / 2f)) * pivotX.rotation;

        var dp = Vector3.Dot(pivotX.up, follow.up); //(int)(Vector3.Dot(pivotX.up, follow.up) * 10000f)/10000f;

        //if(dp!=lastDot)
        // Debug.Log(dp + ", " + lastDot);
        if (dp < .875f)
        {
            //if (lastDot >= dp)
            //{
            pivotX.transform.rotation = origin;
            //Debug.Log("FUCK");
            // }


        }
        // lastDot = dp;

        //pivotY.rotation = originROT;//.FromToRotation(originUP,pivotY.up) * originROT;

        var thetaX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * Sensitivity;
        // thetaX = ((thetaX > .35f) ? .35f : thetaX);
        //thetaX = (thetaX < -.35f ? -.35f : thetaX);
        var rotx = Mathf.Sin(thetaX / 2f) * follow.up.x;
        var roty = Mathf.Sin(thetaX / 2f) * follow.up.y;
        var rotz = Mathf.Sin(thetaX / 2f) * follow.up.z;
        var rotw = Mathf.Cos(thetaX / 2f);
        pivotY.rotation = new Quaternion(rotx, roty, rotz, rotw) * pivotY.rotation;
    }

    private IEnumerator GetCharacter()
    {
        bool done = false;
        while (true)
        {
            cms = GetComponents<CharacterMovement>().ToList();
            foreach (var c in cms)
            {
                if (c.isLocalPlayer)
                {
                    follow = c.transform;
                    character = c;
                    done = true;
                }

            }

            yield return null;
        }

        
    }

}
