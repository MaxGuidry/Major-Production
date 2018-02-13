using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float WalkSpeed = 1;
    public float RunSpeed = 2;
    public GameObject UiInventory;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 position;
    public Planet currentPlanet;

    private Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        UiInventory.SetActive(false);
    }

    void Update()
    {
        // UiInventory.SetActive(Input.GetKey(InputMap.KeyBinds["inventory"]));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // if (Input.GetAxis("A") > 0)
        //     Debug.Log("A");
        // if (Input.GetAxis("X") > 0)
        //     Debug.Log("X");
        // if (Input.GetAxis("B") > 0)
        //     Debug.Log("B");
        // if (Input.GetAxis("Y") > 0)
        //     Debug.Log("Y");
        //if(Input.GetAxis("Mouse X") > 0)
        //    Debug.Log(Input.GetAxis("Mouse X"));
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");
        //if (Input.GetAxis("") > 0)
        //    Debug.Log("");

        float Speed = (Input.GetKey(InputMap.KeyBinds["sprint"])) ? RunSpeed : WalkSpeed;



        Vector3 t = this.transform.forward;

        acceleration = t;

        if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["left"]) &&
            velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }

        else if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["right"]) &&
                 velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["left"]) &&
                 velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["right"]) &&
                 velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }


        else if (Input.GetKey(InputMap.KeyBinds["forward"]) && velocity.magnitude < Speed)
        {

            velocity += acceleration.normalized * velocity.magnitude;
            velocity += acceleration * Time.deltaTime;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["forward"]))
        {

            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["right"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;


        }
        else if (Input.GetKey(InputMap.KeyBinds["right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;



        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && velocity.magnitude < Speed)
        {
            acceleration = -acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["back"]))
        {
            acceleration = -acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["left"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;


        }
        else if (Input.GetKey(InputMap.KeyBinds["left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;

        }
        else
        {
            if (velocity.magnitude < .2f)
                velocity = Vector3.zero;
            Vector3 v = new Vector3(velocity.x, 0, velocity.z);
            velocity += -velocity * ((v.magnitude * 25f) / WalkSpeed) * Time.deltaTime;
        }
        //velocity += ((currentPlanet.center - this.transform.position).normalized * currentPlanet.gravity);
        if (Input.GetKey(InputMap.KeyBinds["forward"]))
        {
            velocity += acceleration * Time.deltaTime;
        }
        //Debug.Log(velocity);



        //rb.AddForce(velocity,ForceMode.VelocityChange);
        this.transform.position += velocity * Time.deltaTime;
        // rb.AddForce(-this.transform.up * 10f);


        //this.transform.position = Vector3.Lerp(this.transform.position,rb.position,.2f);

        //this.transform.position = position;
        //rb.position += position;
        //this.transform.LookAt(this.transform.position + acceleration.normalized);
        //this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .2f);

    }

    void LateUpdate()
    {


        //this.transform.Rotate(this.transform.up, Input.GetAxis("Mouse X"));
        //Vector3 forward = this.transform.forward;
        //this.transform.up = (this.transform.position - currentPlanet.center).normalized;
        //Vector3 up = this.transform.up;
       // this.transform.rotation = Quaternion.LookRotation(forward, this.transform.up);
        //Vector3 t = Vector3.ProjectOnPlane(Camera.main.transform.up, this.transform.up);
        // this.transform.up = (this.transform.position - currentPlanet.center).normalized;
        //Quaternion q = Quaternion.LookRotation(t, this.transform.up);
        // this.transform.rotation = q;// Quaternion.Slerp(this.transform.rotation, q, .25f);
        //Debug.Log(this.transform.up);
        //Debug.DrawLine(this.transform.position, this.transform.position + t, Color.magenta);
        //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.cyan);

    }


    void OnCollisionEnter(Collision collision)
    {

        // Debug.Log("collision");

    }
}
