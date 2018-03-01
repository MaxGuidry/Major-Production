using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public Stats stats;
    public Modifier expModifier;
    /// <summary>
    ///     Entity Takes Damage
    ///     TODO: calculate armor rating using standard rolling system
    ///     Reduce this incoming damage by our armor value
    /// </summary>
    /// <param name="damage">how much damage to take</param>
    public void TakeDamage(int damage)
    {
        var healthStat = stats.GetStat("PHealth");
        var armorStat = stats.GetStat("PArmor");

        var calculatedDamage = damage - armorStat.Value;

        var nexthealth = healthStat.Value - calculatedDamage;

        if (nexthealth <= 0)
        {
            healthStat.Value = nexthealth;
            Die();
        }
        else
        {
            healthStat.Value = nexthealth;
        }
    }

    /// <summary>
    ///     Death of Entity
    ///     TODO: Add more to death
    /// </summary>
    public void Die()
    {
        Debug.Log(transform.name + " died.");
    }

    private void Start()
    {
        expModifier.Value = 0;
        expModifier.Target = null;
    }

    /// <summary>
    ///     Sets a mod and adds and applies that mod
    /// </summary>
    /// <param name="args"></param>
    public void OnQuestcomplete(Object[] args)
    {
        var quest = args[0] as Objective;
        if (quest == null)
            return;
        Debug.Log(quest._reward.ToString());
        var questReward = quest._reward.stat;
        if (questReward == null)
            return;
        var affectedstat = stats.GetStat(questReward.Name);
        if (affectedstat == null)
            return;
        var newaffectedstat = quest._reward.RewardValue;

        expModifier.Value = newaffectedstat;
        expModifier.Target = affectedstat;
        expModifier.Type = ModType.Add;

        affectedstat.AddMod(expModifier);
        affectedstat.ApplyMod(expModifier);
    }
}