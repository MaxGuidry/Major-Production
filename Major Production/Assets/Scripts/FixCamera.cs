using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    private Vector3 StartPosition = new Vector3(0f, 3f, -4.5f);
    public GameObject follow;

    public GameObject lookat;

    public float sensitivity = 1;

    private Vector3 prevPositon;

    private Vector3 prevUp;
    // Use this for initialization
    void Start()
    {
        //this.gameObject.transform.position = follow.transform.position + StartPosition;
        prevPositon = follow.transform.position;
        prevUp = follow.transform.up;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //transform.RotateAround(follow.transform.position,follow.transform.up, Input.GetAxis("Mouse X") * sensitivity);
        //Vector3 deltaFollow = follow.transform.position - prevPositon;
        //this.transform.position += deltaFollow;
        //this.transform.LookAt(follow.transform);

        //float deltaUp = Vector3.Angle(Vector3.ProjectOnPlane(prevUp, new Vector3(1, 0, 0)), Vector3.ProjectOnPlane(follow.transform.up, new Vector3(1, 0, 0)));
        //float sign = Vector3.Dot(follow.transform.forward, deltaFollow.normalized) > 0 ? 1 : -1;
        ////Debug.Log(deltaUp * sign);
        //this.gameObject.transform.RotateAround(follow.transform.position,follow.transform.right, deltaUp * sign);
        //// float angle = (this.transform.rotation.eulerAngles.z - follow.transform.rotation.eulerAngles.z) * Mathf.Deg2Rad * sign;
        //this.gameObject.transform.RotateAround(follow.transform.position, follow.transform.up, Input.GetAxis("Mouse X") * sensitivity);
        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x,
        //    this.transform.rotation.eulerAngles.y, follow.transform.rotation.eulerAngles.z); 
        ////this.transform.Rotate(this.transform.forward,angle);
        ////this.gameObject.transform.LookAt(lookat.transform);


        prevPositon = follow.transform.position;
        prevUp = follow.transform.up;
    }
}
