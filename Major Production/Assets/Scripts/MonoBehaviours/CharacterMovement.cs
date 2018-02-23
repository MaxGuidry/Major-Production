using UnityEngine;

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
    public GameObject cameraPivot;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        //UiInventory.SetActive(false);
    }

    public KeyCode GetKeyCode(string key)
    {
        var k = KeyCode.None;
        InputMap.KeyBinds.TryGetValue(key, out k);

        return k;
    }

    private void Update()
    {
        Sensitivity = InputMap.Sensititivity;
        UiInventory.SetActive(Input.GetKey(InputMap.KeyBinds["inventory"]));
    }

    // Update is called once per frame
    private void FixedUpdate()
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
        //if (Input.GetAxis("Mouse Y") > 0)
        //    Debug.Log(Input.GetAxis("Mouse Y"));
        //if (Input.GetAxis("Right Trigger"))
        //    Debug.Log(Input.GetAxis("Trigger"));
        //if (Input.GetAxis("") > 0)
        // Debug.Log(Input.GetAxis("Left Bumper"));
        // Debug.Log(Input.GetAxis("Right Bumper"));
        //if (Input.GetAxis("") > 0)
        // Debug.Log(Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Vertical"));
        //Debug.Log("Start: " + Input.GetAxis("Start"));
        //Debug.Log("DPad Horizontal: " + Input.GetAxis("DPad Horizontal"));
        //Debug.Log("DPad Vertical: " +Input.GetAxis("DPad Vertical"));
        //Debug.Log("Left Stick Button: " + Input.GetAxis("Left Stick Button"));
        //Debug.Log("Right Stick Button: " + Input.GetAxis("Right Stick Button"));
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

        //KeyCode k_sprint = GetKeyCode("sprint");
        //KeyCode k_forward = GetKeyCode("forward");
        //KeyCode k_left = GetKeyCode("left");
        //KeyCode k_right = GetKeyCode("right");
        //KeyCode k_back = GetKeyCode("back");
        //float Speed = 0.0f;

        UiInventory.SetActive(Input.GetKey(KeyCode.Tab));

        var Speed = Input.GetKey(InputMap.KeyBinds["sprint"]) ? RunSpeed : WalkSpeed;


        var t = transform.forward;
        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        acceleration = t;
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
        var thetaX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * Sensitivity;
        thetaX = ((thetaX > .35f) ? .35f : thetaX);
        thetaX = (thetaX < -.35f ? -.35f : thetaX);
        transform.rotation = new Quaternion(Mathf.Sin(thetaX / 2f) * transform.up.x, Mathf.Sin(thetaX / 2f)
                                                                                               * transform.up.y,
                                 Mathf.Sin(thetaX / 2f) * transform.up.z, Mathf.Cos(thetaX / 2f)) * transform.rotation;
    }

   

    
}