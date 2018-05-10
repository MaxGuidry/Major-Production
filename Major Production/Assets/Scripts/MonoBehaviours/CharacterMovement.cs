﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterMovement : NetworkBehaviour
{
    [SectionHeader("Camera")]
    [Range(1, 20)]
    public float Sensitivity = 1;
    public Transform cameraPivot;

    [SectionHeader("Speeds")]
    public float RunSpeed = 2;
    public float WalkSpeed = 1;

    [SectionHeader("Projectiles")]
    public Transform HandToShoot;
    public GameObject RocketPrefab;
    public GameObject shieldPrefab;

    [SectionHeader("Particles")]
    public GameObject deathParticle;
    public GameObject deathPuff;
    public GameObject takeOff;
    public GameObject landing;
    public GameObject dustKickup;
    public GameObject whirlwind;


    public Animator anim;

    private string PlayerNumber;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private bool grounded = false;
    private bool jumping = false;

    private Vector3 acceleration = Vector3.zero;
    private Coroutine dash;

    [HideInInspector]
    public float rocketCooldown = 0;
    public float MaxRocketCooldown = 8;

    public float whirlwindCooldown = 0;
    public float MaxWhirlwindCooldown = 2;

    public float dashCooldown = 0;
    public float MaxDashCooldown = 2f;

    public float shieldCooldown = 0;
    public float MaxShieldCooldown = 3;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    public enum PlayerState
    {
        Attacking,
        Running,
        None,
    }
    [HideInInspector]
    public PlayerState state;
    [SerializeField] private GameEventArgsObject BreakObject;

    private NetworkManager nm;
    //private Dictionary<string, float> axisValues = new Dictionary<string, float>();

    private void Start()
    {
        state = PlayerState.None;
        var basm = anim.GetBehaviour<BasicAttackSM>();

        basm.Punch = this.GetComponent<AudioSource>().clip;
        basm.source = this.GetComponent<AudioSource>();
        basm.player = this;
        var mssm = anim.GetBehaviour<MissileStrikeSM>();
        mssm.player = this;
        var wwsm = anim.GetBehaviour<WhirlwindSM>();
        wwsm.player = this;
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




        if (Input.GetAxis("Right Bumper" + PlayerNumber) > 0 && state != PlayerState.Attacking)
        {
            if (rocketCooldown < 0)
            {
                anim.SetTrigger("Rocket");
                rocketCooldown = MaxRocketCooldown;
            }
        }
        if (Input.GetAxis("Left Bumper" + PlayerNumber) > 0 && state != PlayerState.Attacking)
            anim.SetBool("Whirlwind", true);
        if (Input.GetAxis("Trigger" + PlayerNumber) < -.9f)
        {
            if (dash == null)
                dash = StartCoroutine(Dash());
        }

        if (Input.GetAxis("Trigger" + PlayerNumber) > .9f && state != PlayerState.Attacking)
        {
            StartCoroutine(Shield());
        }
        //if (Input.GetKeyDown(KeyCode.N) && PlayerNumber == "")
          //  SpawnOnOtherPlanet(FindObjectsOfType<PlanetBehaviour>()[Random.Range(0, 4)]);
        if (GLOBALS.SoloOnline || GLOBALS.SplitscreenOnline)
            if (!isLocalPlayer)
                return;
        if (!cameraPivot)
            return;
        object[] test = { this, "A" };
        if (Input.GetKeyDown(KeyCode.Space))
            Jump(test);

        //RaycastHit rh;
        //Physics.Raycast(this.transform.position, -this.transform.up, out rh, 1.5f);
        //rh.


        var Speed = Input.GetAxis("Left Stick Button" + PlayerNumber) > .1f ? RunSpeed : WalkSpeed;

        var vert = Input.GetAxis("Vertical" + PlayerNumber);
        var hor = Input.GetAxis("Horizontal" + PlayerNumber);

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
        transform.rotation = Quaternion.Slerp(this.transform.rotation, to, .075f);
        // transform.Rotate(to.eulerAngles);
        anim.SetFloat("Velocity", velocity.magnitude * Mathf.Sign(Vector3.Dot(this.transform.forward, velocity.normalized)));
        //Debug.Log(InputManager.Controller());

        rocketCooldown -= Time.deltaTime;
    }



    public void Jump(object[] args)
    {
        // var names = Input.GetJoystickNames();
        // if (names.Contains("Controller (XBOX One For Windows)"))
        // {

        if (args[1] as string == "A" + PlayerNumber)
        {
            //if (!grounded)
            //    return;
            RaycastHit rh;
            Physics.Raycast(transform.position + (transform.up * .2f), -transform.up, out rh, 1, ~LayerMask.GetMask("Player"));
            if (!rh.transform)
            {
                return;

            }

            StartCoroutine(jumpForce());
            rb.AddForce(this.transform.up * 21, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }


        // }
    }

    public IEnumerator jumpForce()
    {
        yield return new WaitForSeconds(.1f);

    }

    public IEnumerator Shield()
    {
        shieldCooldown = MaxShieldCooldown;
        state = PlayerState.Attacking;

        var go = Instantiate(shieldPrefab, this.transform.position, transform.rotation, this.gameObject.transform);
        var stats = GetComponent<PlayerStatBehaviour>();
        stats.Armor += 75;
       
        while (shieldCooldown > 0)
        {
            shieldCooldown -= Time.deltaTime;
            go.transform.rotation = new Quaternion(Mathf.Sin(0.01f) * this.transform.up.x, Mathf.Sin(0.01f) * this.transform.up.y, Mathf.Sin(0.01f) * this.transform.up.z, Mathf.Cos(0.01f)) * go.transform.rotation;
            yield return null;
        }
        Destroy(go);
        state = PlayerState.Running;
        stats.Armor -= 75;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshCollider>())
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
            var mc = other.gameObject.GetComponent<MeshCollider>();
            if (!mc)
                return;
            grounded = true;
            jumping = false;
        }
    }
    void OnCollisionExit(Collision other)
    {

        if (other.gameObject.GetComponent<Collider>())
        {
            var mc = other.gameObject.GetComponent<MeshCollider>();
            if (!mc)
                return;
            grounded = false;
            jumping = true;


        }
    }
    public void HitObject(object[] args)
    {
        if (!anim)
            return;
        if (state == PlayerState.Attacking)
            return;
        if (args.Length < 2)
            return;
        if (args[1] as string == "X" + PlayerNumber)//&& anim.GetCurrentAnimatorStateInfo(). )
        {
            anim.SetTrigger("AttackBasic");

            //BreakObject.ObjRaise(this);
        }
    }

    public void Raycastattack()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(this.transform.position, this.transform.forward, 6, ~LayerMask.GetMask("Player"));
        foreach (var rayhit in hits)
        {
            if (rayhit.transform.gameObject == this.gameObject)
                continue;

            var toplevel = rayhit.transform;
            while (toplevel.parent != null)
            {
                toplevel = toplevel.parent;
            }

            if (toplevel.transform.GetComponent<BreakableResourceBehaviour>() != null)
            {
                var HitObject = toplevel.gameObject.GetComponent<BreakableResourceBehaviour>();
                HitObject.SpawnResources();
                return;
            }

            if (toplevel.transform.GetComponentInChildren<PlayerStatBehaviour>() != null)
            {
                var HitObject = toplevel.gameObject.GetComponentInChildren<PlayerStatBehaviour>();
                //HitObject.TakeDamage(90000);
                return;

            }

        }



    }

    public IEnumerator Dash()
    {

        anim.SetTrigger("Dash");
        float timer = 0;
        while (timer < .5f)
        {
            timer += Time.deltaTime;
            this.transform.position += this.transform.forward * .4f;
            yield return null;
        }

        dashCooldown = MaxDashCooldown;
        while (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
            yield return null;
        }
        dash = null;
    }

    public void FireRocket()
    {
        var go = Instantiate(RocketPrefab, HandToShoot.position, HandToShoot.transform.rotation);
        // go.transform.rotation *= new Quaternion(Mathf.Sin(-0.2f) * go.transform.up.x, Mathf.Sin(-0.2f) * go.transform.up.y, Mathf.Sin(-0.2f) * go.transform.up.z,
        //    Mathf.Cos(-0.2f));
        go.transform.localScale = Vector3.one * .3f;
        var rocketprojectile = go.GetComponent<RocketProjectile>();
        rocketprojectile.PlayerWhoShotMe = this.transform.parent.gameObject.name;
        rocketprojectile.damage = gameObject.GetComponent<PlayerStatBehaviour>().Damage;
    }

    public void SpawnOnOtherPlanet(PlanetBehaviour p)
    {
        var children = p.gameObject.transform.GetComponentsInChildren<Transform>();
        List<GameObject> spawnPoints = new List<GameObject>();
        foreach (var child in children)
        {
            if (child.gameObject.tag != "SpawnPoint")
                continue;
            spawnPoints.Add(child.gameObject);
        }

        var tpLocation = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        this.transform.position = tpLocation;
    }

    public IEnumerator Whirlwind()
    {
        whirlwindCooldown = MaxWhirlwindCooldown;
        var go = Instantiate(whirlwind,Vector3.zero,Quaternion.identity);
        go.transform.parent = this.transform;
       
        while (whirlwindCooldown > 0)
        {
            go.transform.localEulerAngles = new Vector3(-90,0,0);
            go.transform.position = this.transform.position;
            whirlwindCooldown -= Time.deltaTime;
            yield return null;
        }

        anim.SetBool("Whirlwind", false);
        yield return new WaitForSeconds(0.1f);
        Destroy(go);

    }


    public IEnumerator SpawnDelay(PlanetBehaviour p, float timer, WarpBehviour crt, Text warpText, float warpTimer)
    {
        warpText.text = "Taking Off";
        yield return new WaitForSeconds(timer);
        SpawnOnOtherPlanet(p);
        warpText.text = "Landing";
        yield return new WaitForSeconds(timer);
        crt.inputEvents.SetActive(true);
        crt.warping = false;
        enabled = true;
        crt.coroutine = null;
    }

    public void Die()
    {
        anim.SetBool("Death", true);
        cameraPivot = null;
        //Destroy(this.gameObject.GetComponent<CharacterMovement>());
        this.enabled = false;
        this.transform.parent.GetComponentInChildren<GameEventArgsListenerObject>().enabled = false;
        StartCoroutine(deathps());
    }

    IEnumerator deathps()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(deathPuff,this.transform.position,this.transform.rotation);
    }
}