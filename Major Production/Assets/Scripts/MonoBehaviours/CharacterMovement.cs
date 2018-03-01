using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 acceleration = Vector3.zero;
    public Planet currentPlanet;
    private Vector3 position;
    [Range(1, 20)] public float Sensitivity = 1;
    private Rigidbody rb;
    public float RunSpeed = 2;
    public GameObject UiInventory;
    private Vector3 velocity = Vector3.zero;

    public float WalkSpeed = 1;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    
    
    //private Dictionary<string, float> axisValues = new Dictionary<string, float>();

    private void Start()
    {
        
        //foreach (var button in f)
        //{
        //    buttonevents.Add(button.name, button);
        //    axisValues.Add(button.name,0);
        //}
    }

    public KeyCode GetKeyCode(string key)
    {
        var k = KeyCode.None;
        InputMap.KeyBinds.TryGetValue(key, out k);

        return k;
    }
    private void Update()
    {
        //Sensitivity = InputMap.Sensititivity;
        //UiInventory.SetActive(Input.GetKey(InputMap.KeyBinds["inventory"]));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //foreach (var button in buttonevents)
        //{
        //    float value = Input.GetAxis(button.Key);
        //    if (value > .1f)
        //    {
        //        StringVariable sv = ScriptableObject.CreateInstance<StringVariable>();
        //        sv.Value = button.Key;
        //        button.Value.Raise(sv);
        //    }
                
        //    axisValues[button.Key] = value;
        //}
       
        
        //UiInventory.SetActive(Input.GetKey(KeyCode.Tab));

        var Speed = Input.GetKey(InputMap.KeyBinds["sprint"]) ? RunSpeed : WalkSpeed;
       
        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        acceleration = transform.forward;
        var afor = this.transform.forward * ((vert < .1f && vert > -.1f) ? 0 : vert);
        var aright = this.transform.right * ((hor < .1f && hor > -.1f) ? 0 : hor);

        acceleration = afor + aright;

        if (velocity.magnitude < Speed)
        {
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else
        {
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        if (acceleration.magnitude < .01f)
        {
            if (velocity.magnitude < .2f)
                velocity = Vector3.zero;
            var v = new Vector3(velocity.x, 0, velocity.z);
            velocity += -velocity * (v.magnitude * 25f / WalkSpeed) * Time.deltaTime;
        }


        transform.position += velocity * Time.deltaTime;

        //Debug.Log(InputManager.Controller());
        //this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .2f);
        //this.transform.LookAt(this.transform.position + acceleration.normalized);
        //float sens = (Input.GetJoystickNames()[0] == "")
        var thetaX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * Sensitivity;
       // thetaX = ((thetaX > .35f) ? .35f : thetaX);
        //thetaX = (thetaX < -.35f ? -.35f : thetaX);
        var rotx = Mathf.Sin(thetaX / 2f) * transform.up.x;
        var roty = Mathf.Sin(thetaX / 2f) * transform.up.y;
        var rotz = Mathf.Sin(thetaX / 2f) * transform.up.z;
        var rotw = Mathf.Cos(thetaX / 2f);
        transform.rotation = new Quaternion(rotx, roty, rotz, rotw) * transform.rotation;
    }




}