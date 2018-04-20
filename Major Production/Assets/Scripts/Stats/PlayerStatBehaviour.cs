using System.Collections;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public GameObject LevelUpEffect;
    public GameEventArgs LevelUpEvent;
    public Stats stats;

    public int Health, Armor, Level;

    private void Awake()
    {
        stats._stats.ForEach(stat => stat.Value = 0);
        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                Health = stats.GetStat("PHealth").Value = 100;
                break;
            case "P2":
                Health = stats.GetStat("PHealth 1").Value = 100;
                break;
            case "P3":
                Health = stats.GetStat("PHealth 2").Value = 100;
                break;
            case "P4":
                Health = stats.GetStat("PHealth 3").Value = 100;
                break;
            default:
                break;
        }
        UpdateStat();
    }

    private void UpdateStat()
    {
        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                Health = stats.GetStat("PHealth").Value;
                Armor = stats.GetStat("PArmor").Value;
                Level = stats.GetStat("PLevel").Value;
                break;
            case "P2":
                Health = stats.GetStat("PHealth 1").Value;
                Armor = stats.GetStat("PArmor 1").Value;
                Level = stats.GetStat("PLevel 1").Value;
                break;
            case "P3":
                Health = stats.GetStat("PHealth 2").Value;
                Armor = stats.GetStat("PArmor 2").Value;
                Level = stats.GetStat("PLevel 2").Value;
                break;
            case "P4":
                Health = stats.GetStat("PHealth 3").Value;
                Armor = stats.GetStat("PArmor 3").Value;
                Level = stats.GetStat("PLevel 3").Value;
                break;
        }
    }
    /// <summary>
    ///     Entity Takes Damage
    ///     TODO: calculate armor rating using standard rolling system
    ///     Reduce this incoming damage by our armor value
    /// </summary>
    /// <param name="damage">how much damage to take</param>
    public void TakeDamage(int damage)
    {
        var calculatedDamage = damage - Armor;

        var nexthealth = Health - calculatedDamage;

        if (nexthealth <= 0)
        {
            Health = 0;
            Die();
        }
        else
        {
            Health = nexthealth;
            
        }

    }

    /// <summary>
    ///     Death of Entity
    ///     TODO: Add more to death
    /// </summary>
    public void Die()
    {  
        this.gameObject.GetComponent<CharacterMovement>().Die();
    }

    /// <summary>
    ///     Sets a mod and adds and applies that mod
    /// </summary>
    /// <param name="args"></param>
    public void OnQuestComplete(Object[] args)
    {
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
            switch (GetComponent<Transform>().tag)
            {
                case "P1":
                    stats.GetStat("PLevel").Value++;
                    break;
                case "P2":
                    stats.GetStat("PLevel 1").Value++;
                    break;
                case "P3":
                    stats.GetStat("PLevel 2").Value++;
                    break;
                case "P4":
                    stats.GetStat("PLevel 3").Value++;
                    break;
            }
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