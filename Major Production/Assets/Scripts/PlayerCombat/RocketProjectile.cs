using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{

    public float rocketSpeed;
    public float upSpeed;
    private Rigidbody rb;
    [HideInInspector] public string PlayerWhoShotMe="";
    public GameObject ExplosionPrefab;

    public float damage;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyTime());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //this.transform.position += this.transform.up *(upSpeed /100f);
        //this.transform.position += this.transform.forward *(rocketSpeed/100f);
        if (rb.velocity.magnitude < 17)
            rb.AddForce(transform.forward * (rocketSpeed / 10f), ForceMode.Impulse);
        else
        {
            rb.velocity = rb.velocity.normalized * 17f;
        }
        rb.AddForce(transform.up * (upSpeed / 100f), ForceMode.Impulse);


    }

    void Update()
    {
        if (transform.localScale.x < 2)
            transform.localScale *= 1.1f;
    }
    void OnCollisionEnter(Collision other)
    {
        //explosion

        Destroy(this.gameObject);

    }
    void OnTriggerEnter(Collider other)
    {
        //explosion
        if (other.gameObject.GetComponent<PlanetBehaviour>() != null)
            return;
        var player = other.gameObject.transform.root; //GLOBALS.GetTopLevelParentTransform(other.gameObject.transform);
        if (player.gameObject.name.Contains("Player"))
        {
            
            if (player.gameObject.name == PlayerWhoShotMe)
                return;
        }
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {

        var go = Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
        Collider[] knockbacks = Physics.OverlapSphere(this.transform.position, 6);
        for (int i = 0; i < knockbacks.Length; i++)
        {
            var rb = knockbacks[i].gameObject.GetComponent<Rigidbody>();
            if (!rb)
                continue;
            if (rb == this.rb)
                continue;
            rb.AddExplosionForce(13, this.transform.position - this.transform.up, 6, 0, ForceMode.Impulse);
            if (!rb.gameObject.name.Contains("Rock"))
                Debug.Log(rb.gameObject.name);
            var id = rb.gameObject.GetComponent<IDamageable>();
            if (id == null)
                continue;
            int dmg = (int)(-(rb.transform.position - transform.position).magnitude + (damage/2f));

            id.TakeDamage(dmg);
        }
    }

    //void OnCollisionStay(Collision other)
    //{
    //    //explosion
    //    Collider[] knockbacks = Physics.OverlapSphere(this.transform.position, 15);
    //    for (int i = 0; i < knockbacks.Length; i++)
    //    {
    //        var rb = knockbacks[i].gameObject.GetComponent<Rigidbody>();
    //        if (!rb)
    //            continue;
    //        if (rb == this.rb)
    //            continue;
    //        rb.AddExplosionForce(15, this.transform.position-this.transform.up, 15, 0, ForceMode.Impulse);
    //
    //        var id = rb.gameObject.GetComponent<IDamageable>();
    //        if (id == null)
    //            continue;
    //        id.TakeDamage((int)(50 / (15 - (this.transform.position - rb.transform.position).magnitude)));
    //
    //    }
    //    Destroy(this.gameObject);
    //}
    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);

    }
}
