using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

public class FixCamera : MonoBehaviour
{
    private string PlayerNumber;
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

        this.transform.position =  Vector3.Lerp(this.transform.position,follow.transform.position,.15f);
        this.transform.rotation =Quaternion.Slerp(this.transform.rotation, Quaternion.FromToRotation(this.transform.up, follow.transform.up) * this.transform.rotation,.5f);// follow.transform.rotation;
                                                                                                                              // this.transform.rotation = Quaternion.FromToRotation(this.transform.right, follow.transform.right) * this.transform.rotation;// follow.transform.rotation;
        Quaternion origin = pivotX.rotation;
        if (character == null)
            return;
        Sensitivity = character.Sensitivity;
        var thetaY = Input.GetAxis("Mouse Y" + PlayerNumber) * Mathf.Deg2Rad * Sensitivity * .5f;
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
            dp = Vector3.Dot(pivotX.up, follow.up);
            if (dp < .875f)
            {
                   
                pivotX.rotation = new Quaternion(Mathf.Sin(-.01f) * pivotX.right.x, Mathf.Sin(-.01f)
                                                                                    * pivotX.right.y,
                                      Mathf.Sin(-.01f) * pivotX.right.z, Mathf.Cos(-.01f)) * pivotX.rotation;
                var newdp = Vector3.Dot(pivotX.up, follow.up);
                if (newdp < dp)
                {
                    pivotX.transform.rotation = origin;
                    pivotX.rotation = new Quaternion(Mathf.Sin(.01f) * pivotX.right.x, Mathf.Sin(.01f)
                                                                                        * pivotX.right.y,
                                          Mathf.Sin(.01f) * pivotX.right.z, Mathf.Cos(.01f)) * pivotX.rotation;
                }

            }

            //Debug.Log("FUCK");
            // }
        }
        // lastDot = dp;

        //pivotY.rotation = originROT;//.FromToRotation(originUP,pivotY.up) * originROT;

        var thetaX = Input.GetAxis("Mouse X" + PlayerNumber) * Mathf.Deg2Rad * Sensitivity;
        //thetaX = ((thetaX > .35f) ? .35f : thetaX);
        //thetaX = (thetaX < -.35f ? -.35f : thetaX);
        var rotx = Mathf.Sin(thetaX / 2f) * follow.up.x;
        var roty = Mathf.Sin(thetaX / 2f) * follow.up.y;
        var rotz = Mathf.Sin(thetaX / 2f) * follow.up.z;
        var rotw = Mathf.Cos(thetaX / 2f);
        pivotY.rotation = new Quaternion(rotx, roty, rotz, rotw) * pivotY.rotation;
    }

    public IEnumerator GetCharacter()
    {
        bool done = false;
        string playernumber = "";
        while (!done)
        {
            if (GLOBALS.SplitscreenLocal || GLOBALS.SplitscreenOnline)
                playernumber = this.gameObject.transform.parent.GetChild(0).name;
            cms = GameObject.FindObjectsOfType<CharacterMovement>().ToList();

            foreach (var c in cms)
            {
               
                if (GLOBALS.SplitscreenLocal || GLOBALS.SplitscreenOnline)
                {
                    if (playernumber != c.gameObject.name)
                        continue;
                }

                if (c.cameraPivot == pivotY)
                {
                    if (!follow)
                        follow = c.transform;
                    if (!c.cameraPivot)
                        c.cameraPivot = pivotY;
                    if (!character)
                        character = c;
                    done = true;

                    switch (c.gameObject.tag)
                    {
                        case "P1":
                            PlayerNumber = "";
                            break;
                        case "P2":
                            PlayerNumber = "1";
                            break;
                        case "P3":
                            PlayerNumber = "2";
                            break;
                        case "P4":
                            PlayerNumber = "3";
                            break;


                    }

                    break;
                }

            }

            yield return new WaitForSeconds(.1f);
        }


    }

}
