using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour, IDamageable
{
    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    public int Health
    {
        get { return stats["Health"].Value; }
        set { stats["Health"].Value = value; }
    }

    public int Armor
    {
        get { return stats["Armor"].Value; }
        set { stats["Armor"].Value = value; }
    }

    public int Damage
    {
        get { return stats["Damage"].Value; }
        set { stats["Damage"].Value = value; }
    }

    /// <summary>
    /// Entity Takes Damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        damage -= Armor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        Health -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (Health <= 0)
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
