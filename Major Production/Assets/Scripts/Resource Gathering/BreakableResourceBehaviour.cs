using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableResourceBehaviour : MonoBehaviour,IDamageable
{


    public GameObject Resource;
    // Use this for initialization
    private ItemObjectPooler pooler;

    void Start()
    {
        pooler = FindObjectOfType<ItemObjectPooler>();
    }

    public void SpawnResources(object[] args)
    {
        if ((args[0]) as CharacterMovement == null)
            return;
        int r = Random.Range(1, 4);
        for (int i = 0; i < r; i++)
        {
            var obj = ItemObjectPooler.s_instance.Create(Resource, this.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(1, 3), Random.Range(-3, 3)), Quaternion.identity);
            obj.SetActive(true);
            var rb = obj.GetComponent<Rigidbody>();
            if (!rb)
                return;
            rb.AddForce((obj.transform.position - this.transform.position) + new Vector3(0, 1, 0));
        }
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