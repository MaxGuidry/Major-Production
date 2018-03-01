using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public Stats stats;
    /// <summary>
    /// Entity Takes Damage
    /// </summary>
    /// <param name="damage">how much damage to take</param>
    public void TakeDamage(int damage)
    {
        Stat healthStat = stats.GetStat("Health");
        Stat armorStat = stats.GetStat("Armor");
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
    /// <summary>
    /// Death of Entity
    /// </summary>
    public void Die()
    {
        Debug.Log(transform.name + " died.");
    }

    public void OnQuestcomplete(UnityEngine.Object[] args)
    {
        var quest = args[0] as Objective;
        if (quest == null)
            return;
        Debug.Log("i did it!" + quest._reward.ToString());
        var affectedstat = stats.GetStat(quest._reward.stat.Name);
        var newaffectedstat = affectedstat.Value + quest._reward.stat.Value;
    }
}
