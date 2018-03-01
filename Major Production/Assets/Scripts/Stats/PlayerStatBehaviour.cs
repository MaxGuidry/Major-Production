using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public Stats stats;
    public Modifier expModifier;

    void Start()
    {
        expModifier.Value = 0;
    }
    /// <summary>
    /// Entity Takes Damage
    /// </summary>
    /// <param name="damage">how much damage to take</param>
    public void TakeDamage(int damage)
    {
        var healthStat = stats.GetStat("PHealth");
        var armorStat = stats.GetStat("PArmor");
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
        Debug.Log(quest._reward.ToString());
        var questReward = quest._reward.stat;
        if (questReward == null)
            return;
        var affectedstat = stats.GetStat(questReward.Name);
        var newaffectedstat = affectedstat.Value + quest._reward.RewardValue;

        expModifier.Value = newaffectedstat;
        expModifier.Target = affectedstat;
        expModifier.Type = ModType.Add;

        affectedstat.AddMod(expModifier);
        affectedstat.ApplyMod(expModifier);
    }
}
