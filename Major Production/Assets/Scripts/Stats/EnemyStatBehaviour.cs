using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatBehaviour : MonoBehaviour, IDamageable {

    public Stats stats;

    public void TakeDamage(int damage)
    {
        var healthStat = stats.GetStat("EHealth");
        var armorStat = stats.GetStat("EArmor");
        //TODO: calculate armor rating using standard rolling system
        //reduce this incoming damage by our armor value 
        var calculatedDamage = damage - armorStat.Value;

        var nexthealth = healthStat.Value - calculatedDamage;

        if (nexthealth <= 0)
        {
            healthStat.Value = nexthealth;
            Die();
        }
        else
            healthStat.Value = nexthealth;
    }

    public void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
