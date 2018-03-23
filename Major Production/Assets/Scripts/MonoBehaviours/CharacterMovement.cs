using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour
{

    private string PlayerNumber;
    private Vector3 acceleration = Vector3.zero;
    public Planet currentPlanet;
    [Range(1, 20)] public float Sensitivity = 1;
    private Rigidbody rb;
    public float RunSpeed = 2;
    private Vector3 velocity = Vector3.zero;
    private bool grounded = false;
    private bool jumping = false;
    public float WalkSpeed = 1;
    public Transform cameraPivot;
    [SerializeField] private GameEventArgsObject BreakObject;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }


    public Animator anim;

    private NetworkManager nm;
    //private Dictionary<string, float> axisValues = new Dictionary<string, float>();

    private void Start()
    {
        nm = FindObjectOfType<NetworkManager>();
        if (!anim)
            anim = GetComponent<Animator>();
        //if (!cameraPivot)
         //   cameraPivot = Camera.main.transform.parent.parent;

        switch (gameObject.tag)
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
        if (!isLocalPlayer && nm)
            return;
        if (!cameraPivot)
            return;
        object[] test = { this, "A" };
        if (Input.GetKeyDown(KeyCode.Space))
            Jump(test);

        //RaycastHit rh;
        //Physics.Raycast(this.transform.position, -this.transform.up, out rh, 1.5f);
        //rh.


        var Speed = Input.GetKey(InputMap.KeyBinds["sprint"]) ? RunSpeed : WalkSpeed;

        var vert = Input.GetAxis("Vertical" + PlayerNumber);
        var hor = Input.GetAxis("Horizontal"+PlayerNumber);

        acceleration = cameraPivot.transform.forward;
        var afor = cameraPivot.transform.forward * ((vert < .1f && vert > -.1f) ? 0 : vert);
        var aright = cameraPivot.transform.right * ((hor < .1f && hor > -.1f) ? 0 : hor);

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



        transform.position += (velocity * Time.deltaTime);
        //rb.MovePosition(pos);
        Quaternion to = Quaternion.FromToRotation(this.transform.forward, velocity.normalized) * this.transform.rotation;
        transform.rotation = Quaternion.Slerp(this.transform.rotation, to, .1f);
       // transform.Rotate(to.eulerAngles);
        anim.SetFloat("Velocity", velocity.magnitude * Mathf.Sign(Vector3.Dot(this.transform.forward, velocity.normalized)));
        //Debug.Log(InputManager.Controller());
        //this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .2f);
        //this.transform.LookAt(this.transform.position + acceleration.normalized);
        //float sens = (Input.GetJoystickNames()[0] == "")
        //var thetaX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * Sensitivity * Time.deltaTime * 60;
        // thetaX = ((thetaX > .35f) ? .35f : thetaX);
        //thetaX = (thetaX < -.35f ? -.35f : thetaX);
        //var rotx = Mathf.Sin(thetaX / 2f) * transform.up.x;
        //var roty = Mathf.Sin(thetaX / 2f) * transform.up.y;
        //var rotz = Mathf.Sin(thetaX / 2f) * transform.up.z;
        //var rotw = Mathf.Cos(thetaX / 2f);
        //transform.rotation = new Quaternion(rotx, roty, rotz, rotw) * transform.rotation;
    }



    public void Jump(object[] args)
    {
        // var names = Input.GetJoystickNames();
        // if (names.Contains("Controller (XBOX One For Windows)"))
        // {
        if (!grounded)
            return;
        if (args[1] as string == "A"+PlayerNumber)
        {
            if (!grounded)
                return;
            grounded = false;

            rb.AddForce(this.transform.up * 25, ForceMode.Impulse);
            jumping = true;
        }


        // }
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
            jumping = false;
        }
    }

    void OnCollisionStay(Collision other)
    {

        if (other.gameObject.GetComponent<Collider>())
        {
            //needs check for if the object is below the player relative to the players up axis.
            if (!jumping)
            {
                grounded = true;
                //jumping = false;

            }
        }
    }
    public void HitObject(object[] args)
    {
        if (args.Length < 2)
            return;
        if (args[1] as string == "X" + PlayerNumber)
        {
            anim.SetTrigger("AttackBasic");
            //BreakObject.ObjRaise(this);
        }
    }

    public void Raycastattack()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(this.transform.position, this.transform.forward, 6, ~LayerMask.GetMask("Player"));
        GameObject breakableObj = null;
        foreach (var rayhit in hits)
        {
            var toplevel = rayhit.transform;
            while (toplevel.parent != null)
            {
                toplevel = toplevel.parent;
            }
            if (toplevel.transform.GetComponent<BreakableResourceBehaviour>() == null)
                continue;
            breakableObj = toplevel.gameObject;
        }


        if (!breakableObj)
            return;
        var breakable = breakableObj.transform.GetComponent<BreakableResourceBehaviour>();
        if (breakable)
        {
            breakable.SpawnResources();
        }
    }

}