using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour {
    public float walkSpeed;
    public float runSpeed;

    public float turnSmoothTime;
    float turnSmoothVelocity;

    public float speedSmoothTime;
    float speedSmoothVelocity;
    float currentSpeed;

    Transform camTransform;
	// Use this for initialization
	void Start () {
        walkSpeed = 2;
        runSpeed = 6;

        turnSmoothTime = 0.2f;
        speedSmoothTime = 0.1f;
        camTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var inputDir = input.normalized;

        if (inputDir != Vector3.zero)
        {
            var targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        var running = Input.GetKey(KeyCode.LeftShift);
        var targetSpeed = ((running) ? runSpeed: walkSpeed) *inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
	}
}
