using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float WalkSpeed = 1;
    public float RunSpeed = 2;

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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion origin = this.transform.rotation;
        //this.transform.forward = Camera.main.transform.forward;
        //int i = 0;
        //while (Vector3.Dot((this.transform.position - currentPlanet.center).normalized, this.transform.up) < .9f)

        //{
        //    this.transform.rotation *= new Quaternion(Mathf.Sin(5f / 2f), 0, 0, Mathf.Cos(5 / 2f));
        //    i++;
        //    if (i > 200)
        //        break;
        //}

        this.transform.up = (this.transform.position - currentPlanet.center).normalized;
        //Vector3 cross = Vector3.Cross(new Vector3(0,1,0),this.transform.up);
        //Vector3 forward = Vector3.Cross(this.transform.up,cross);
        //Debug.Log(forward);
        //this.transform.forward = forward;
        float Speed = (Input.GetKey(InputMap.KeyBinds["sprint"])) ? RunSpeed : WalkSpeed;
        Vector3 t = Vector3.ProjectOnPlane(Camera.main.transform.forward, this.transform.up);
        acceleration = t; //Camera.main.transform.forward;//new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["left"]) && velocity.magnitude < Speed)
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

        else if (Input.GetKey(InputMap.KeyBinds["forward"]) && Input.GetKey(InputMap.KeyBinds["right"]) && velocity.magnitude < Speed)
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
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["left"]) && velocity.magnitude < Speed)
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
        else if (Input.GetKey(InputMap.KeyBinds["back"]) && Input.GetKey(InputMap.KeyBinds["right"]) && velocity.magnitude < Speed)
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
        rb.AddForce(((currentPlanet.center - this.transform.position).normalized * currentPlanet.gravity));
        // rb.AddForce(-this.transform.up * 10f);
        Debug.DrawLine(this.transform.position, this.transform.position + ((currentPlanet.center - this.transform.position)));
        Debug.DrawLine(this.transform.position, this.transform.position + velocity, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.position + acceleration, Color.blue);
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.cyan);
        //this.transform.position = Vector3.Lerp(this.transform.position,rb.position,.2f);
        
        //this.transform.position = position;
        //rb.position += position;
        Quaternion q = this.transform.rotation;
        this.transform.LookAt(this.transform.position + acceleration.normalized);
        this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .2f);

    }

    void OnCollisionEnter(Collision collision)
    {

        // Debug.Log("collision");

    }
}
