using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    public List<Rigidbody> rbs = new List<Rigidbody>();
    public Planet planet;
    public List<Rigidbody> rbrs = new List<Rigidbody>();

    // Use this for initialization
    void Awake()
    {
        planet.Initialize(this.transform.position, this.transform.localScale.x / 2f, 5000);
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.transform.position = planet.center;
        //g.transform.localScale = new Vector3(.1f,.1f,.1f);
    }
    void Start()
    {
        rbs = GameObject.FindObjectsOfType<Rigidbody>().ToList();
        foreach (var rb in rbs)
        {

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rbs = GameObject.FindObjectsOfType<Rigidbody>().ToList();
        foreach (var rb in rbs)
        {
            Attract(rb);
        }

        foreach (var rbr in rbrs)
        {
            rbs.Remove(rbr);
        }
    }

    void Attract(Rigidbody rb)
    {
        if (!rb)
        {
            rbrs.Add(rb);
            return;
        }




        //GET VERTS SORT AND FIX VELOCITY
        if (rb.GetComponent<CharacterMovement>() != null)
        {
            var mc = GetComponentInChildren<MeshCollider>();
            var verts = mc.sharedMesh.vertices.ToList();

            //verts = verts.OrderBy(( Vector3 x,Vector3 y) => Vector3.Distance(rb.transform.position, x).CompareTo(Vector3.Distance(rb.transform.position,y)));
            var newlist = verts.OrderBy(x => Vector3.Distance(rb.transform.position, x + planet.center)).ToList();

            var closestvert = newlist[0];
            Debug.Log(closestvert + "|||||" + (closestvert + planet.center));
            rb.transform.rotation =
                Quaternion.FromToRotation(rb.transform.up, rb.transform.position - (closestvert + planet.center)) *
                rb.transform.rotation;
            rb.AddForce((((closestvert + planet.center) - rb.transform.position).normalized * planet.gravity) / .2f);
        }
        //var cent = mc.ClosestPoint(rb.gameObject.transform.position);

        //if ((rb.transform.position - this.GetComponent<Collider>().ClosestPoint(rb.transform.position)).magnitude > 1.25f)
        //    rb.AddForce(((planet.center - rb.transform.position).normalized * planet.gravity) / .2f);
        //else
        //{
        //    rb.AddForce(((planet.center - rb.transform.position).normalized * (.001f * planet.gravity)) / .2f);

        //}
        //if (rb.GetComponent<CharacterMovement>() != null)
        //{
        //    RaycastHit[] rh = Physics.RaycastAll(new Ray(rb.transform.position, -rb.transform.up), 1.2f);
        //    bool move = false;
        //    foreach (var hit in rh)
        //    {
        //        if (hit.collider == rb.GetComponent<Collider>())
        //            continue;
        //        PlanetBehaviour pb = hit.transform.GetComponent<PlanetBehaviour>();
        //        if (!pb)
        //        {
        //            rb.AddForce(((planet.center - rb.transform.position).normalized * planet.gravity) / .2f);
        //            return;
        //        }
        //        else
        //        {
        //            Debug.Log("grounded");
        //            rb.AddForce(((planet.center - rb.transform.position).normalized * (.001f * planet.gravity)) / .2f);
        //            return;
        //        }
        //    }
        //}


        else
        {
            //ORIGINAL CODE:
            rb.transform.rotation = Quaternion.FromToRotation(rb.transform.up, rb.transform.position - planet.center) *
                                     rb.transform.rotation;
            rb.AddForce(((planet.center - rb.transform.position).normalized * planet.gravity) / .2f);

            //END ORIGINAL CODE.

        }


        //float y = rb.transform.rotation.eulerAngles.y;
        //rb.transform.up = (rb.gameObject.transform.position - planet.center).normalized;
        //rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, y, rb.transform.rotation.eulerAngles.z);
        //rb.gameObject.transform.rotation = Quaternion.LookRotation(f, up);


    }

}
