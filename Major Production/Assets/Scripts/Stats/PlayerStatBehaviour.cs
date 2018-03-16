using System.Collections;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public GameObject LevelUpEffect;
    public GameEventArgs LevelUpEvent;
    public Stats stats;

    private void Start()
    {
        stats._stats.ForEach(stat => stat.Value = 0);
        stats.GetStat("PHealth").Value = 100;
    }

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
    
    /// <summary>
    ///     Start Coroutine 
    /// </summary>
    public void StartSpawnEffect()
    {
        StartCoroutine(SpawnEffect());
    }

    /// <summary>
    ///     Instatiates LevelUpEffect 
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEffect()
    {
        var done = false;
        while (!done)
        {
            var effect = Instantiate(LevelUpEffect, Vector3.zero, Quaternion.identity);
            effect.gameObject.transform.SetParent(gameObject.transform);
            effect.transform.localRotation = Quaternion.identity;
            effect.transform.localPosition = Vector3.zero;
            effect.transform.localPosition = new Vector3(
                effect.gameObject.transform.localPosition.x,
                -1f,
                effect.gameObject.transform.localPosition.z);
            foreach (var eff in effect.GetComponentsInChildren<Transform>())
                eff.transform.localScale = new Vector3(.5f, .5f, .5f);
            yield return new WaitForSeconds(3);
            done = true;
            Destroy(effect);
        }
    }
}