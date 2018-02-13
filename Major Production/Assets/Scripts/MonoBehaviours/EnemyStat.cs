using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable {
    public int maxHealth = 100;
    public int CurrentHealth
    {
        get;
        private set;
    }

    public Stat DamageStat;
    public Stat ArmorStat;
    // Use this for initialization
    void Start () {
        CurrentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        damage -= ArmorStat.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
