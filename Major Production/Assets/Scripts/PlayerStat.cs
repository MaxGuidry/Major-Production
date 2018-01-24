using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int CurrentHealth
    {
        get;
        private set;
    }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(10);
    }
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
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
