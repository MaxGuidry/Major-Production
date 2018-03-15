using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreakableResourceBehaviour : MonoBehaviour,IDamageable
{


    public GameObject Resource;
    // Use this for initialization
    private int hits;
    public int maxHits;
    void Start()
    {
    }

    public void SpawnResources()
    {
        if (hits > maxHits)
            return;
        
        int r = Random.Range(1, 2);
        for (int i = 0; i < r; i++)
        {
            var obj = ItemObjectPooler.s_instance.Create(Resource, this.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(1, 3), Random.Range(-3, 3)), Quaternion.identity);
            obj.SetActive(true);
            var rb = obj.GetComponent<Rigidbody>();
            if (!rb)
                return;
            rb.AddForce((obj.transform.position - this.transform.position) + new Vector3(0, 1, 0));
        }

        hits++;
        if(hits > maxHits)
            StartCoroutine(DestroyBreakable());
    }

    public IEnumerator DestroyBreakable()
    {
        var peices =  this.gameObject.transform.GetComponentsInChildren<Transform>().ToList();
        foreach (var peice in peices)
        {
            var rb = peice.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddExplosionForce(200, rb.transform.position, 7);
            rb.Sleep();
        }

        
       
        

        yield return new WaitForSeconds(2);
        ItemObjectPooler.s_instance.Destroy(this.gameObject);

    }
    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}