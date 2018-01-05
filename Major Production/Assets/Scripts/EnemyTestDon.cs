using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestDon : MonoBehaviour, IDamageable
{
    public EntityStats EnemyTest;

    public int Damage;
    public float InitalStatAmount;
    // Use this for initialization
    void Start () {
        //foreach (var stat in EnemyTest.NeedsList)
        //{
        //    stat = InitalStatAmount;
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
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
