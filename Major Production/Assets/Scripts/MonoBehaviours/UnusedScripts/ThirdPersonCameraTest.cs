using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraTest : MonoBehaviour {
    public bool lockCursor;
    public float MouseSensitivity;
    public Transform target;
    public float Distance;
    [Header("Rotation")]
    public float rotationSmooth;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public Vector2 camMinMax;
    float horMouse;
    float vertMouse;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        MouseSensitivity = 5;
        Distance = 5;
        target = GameObject.FindGameObjectWithTag("CamTarget").transform;
        camMinMax = new Vector2(-12, 85);
        rotationSmooth = .12f;
    }

    private void LateUpdate()
    {
        //Get Mouse Input 
        horMouse += Input.GetAxis("Mouse X") * MouseSensitivity;
        vertMouse -= Input.GetAxis("Mouse Y") * MouseSensitivity;

        //Lock from rotating too far around
        vertMouse = Mathf.Clamp(vertMouse, camMinMax.x, camMinMax.y);

        //Rotate camera 
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(vertMouse, horMouse), ref rotationSmoothVelocity, rotationSmooth);
        transform.eulerAngles = currentRotation;

        //Lock on to the target
        transform.position = target.position - transform.forward * Distance;
    }
}
