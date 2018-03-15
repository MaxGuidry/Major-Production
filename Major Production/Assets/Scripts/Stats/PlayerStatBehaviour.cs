using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public Stats stats;
    public GameEventArgs LevelUpEvent;
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

    /// <summary>
    ///     Sets a mod and adds and applies that mod
    /// </summary>
    /// <param name="args"></param>
    public void OnQuestComplete(Object[] args)
    {
        var levelStat = stats.GetStat("PLevel");
        var quest = args[0] as Objective;
        if (quest == null)
            return;

        Debug.Log(quest._reward.ToString());

        var questReward = quest._reward.stat;

        if (questReward == null) return;

        var affectedstat = stats.GetStat(questReward.Name);

        if (affectedstat == null) return;

        var newaffectedstat = quest._reward.RewardValue;

        var expMod = Modifier.CreateModifier(newaffectedstat, affectedstat, ModType.Add);

        affectedstat.AddMod(expMod);
        affectedstat.ApplyMod(expMod);

        if (affectedstat.Value >= 100)
        {
            affectedstat.Value -= 100;
            levelStat.Value++;
            LevelUpEvent.Raise(this);
        }
    }
}