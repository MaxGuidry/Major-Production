using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour, IDamageable
{
    /// <summary>
    /// Not a stat, cannot find a reason to add modifiers to health
    /// </summary>
    public int maxHealth = 100;
    public int CurrentHealth
    {
        get;
        private set;
    }
    public Stat DamageStat;
    public Stat ArmorStat;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Entity Takes Damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        damage -= ArmorStat.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0)
            Die();
    }
    /// <summary>
    /// Death of Entity
    /// </summary>
    public void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
