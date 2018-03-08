using System.Collections.Generic;
using System.Linq;
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
    private Vector3 velocity = Vector3.zero;
    private bool grounded = false;
    public float WalkSpeed = 1;
    [SerializeField] private GameEventArgsObject BreakObject;
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

    // Update is called once per frame
    private void Update()
    {

        //FOR TESTING WHEN ANIMATIONS ARE IN THIS WILL BE CALLED ON AN ANIMATION EVENT
        if(Input.GetAxis("X")>0||Input.GetKeyDown(KeyCode.Mouse0))
            HitObject();




        var Speed = Input.GetKey(InputMap.KeyBinds["sprint"]) ? RunSpeed : WalkSpeed;

        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        acceleration = transform.forward;
        var afor = this.transform.forward * ((vert < .1f && vert > -.1f) ? 0 : vert);
        var aright = this.transform.right * ((hor < .1f && hor > -.1f) ? 0 : hor);

        acceleration = (afor + aright) * 10;

        if (velocity.magnitude < Speed)
        {
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration * Time.deltaTime;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else
        {
            velocity += acceleration * Time.deltaTime;
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
        var thetaX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * Sensitivity * Time.deltaTime * 60;
        // thetaX = ((thetaX > .35f) ? .35f : thetaX);
        //thetaX = (thetaX < -.35f ? -.35f : thetaX);
        var rotx = Mathf.Sin(thetaX / 2f) * transform.up.x;
        var roty = Mathf.Sin(thetaX / 2f) * transform.up.y;
        var rotz = Mathf.Sin(thetaX / 2f) * transform.up.z;
        var rotw = Mathf.Cos(thetaX / 2f);
        transform.rotation = new Quaternion(rotx, roty, rotz, rotw) * transform.rotation;
    }



    public void Jump(object[] args)
    {
        var names = Input.GetJoystickNames();
        if (names.Contains("Controller (XBOX One For Windows)"))
        {
            {
                if (args[1] as string == "A")
                {
                    if (!grounded)
                        return;
                    rb.AddForce(this.transform.up * 25, ForceMode.Impulse);
                    grounded = false;

                }
            }

        }
    }
    //todo MAKE SURE THE DELTA TIME IS CORRECT
    //
    //
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Collider>())
        {
            //needs check for if the object is below the player relative to the players up axis.
            grounded = true;
        }
    }

    public void HitObject()
    {

        BreakObject.ObjRaise(this);
    }

}