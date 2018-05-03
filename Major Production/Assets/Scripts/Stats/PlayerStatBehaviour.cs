using System.Collections;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour, IDamageable
{
    public GameObject LevelUpEffect;
    public GameEventArgs LevelUpEvent;
    public Stats stats;

    public int Health, Armor, Level, Speed, Damage, EXP;

    private void Awake()
    {
        stats._stats.ForEach(stat => stat.Value = 0);
        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                SetInitStats("", 100, 40, 50, 70);
                break;
            case "P2":
                SetInitStats(" 1", 100, 40, 50, 70);
                break;
            case "P3":
                SetInitStats(" 2", 100, 40, 50, 70);
                break;
            case "P4":
                SetInitStats(" 3", 100, 40, 50, 70);
                break;
            default:
                break;
        }
        UpdateStat();
    }

    private void SetInitStats(string number, int HealthAmount, int ArmorAmount, int SpeedAmount, int DamageAmount)
    {
        Level = stats.GetStat("PLevel" + number).Value = 1;
        Health = stats.GetStat("PHealth" + number).Value = HealthAmount;
        Armor = stats.GetStat("PArmor" + number).Value = ArmorAmount;
        Speed = stats.GetStat("PSpeed" + number).Value = SpeedAmount;
        Damage = stats.GetStat("PDamage" + number).Value = DamageAmount;
    }

    private void Update()
    {
        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                if (stats.GetStat("PExperience").Value >= 100)
                {
                    LevelUpEvent.Raise(this);
                    stats.GetStat("PExperience").Value -= 100;
                    stats.GetStat("PLevel").Value++;
                    UpdateStat();
                }
                break;
            case "P2":
                if (stats.GetStat("PExperience 1").Value >= 100)
                {
                    LevelUpEvent.Raise(this);
                    stats.GetStat("PExperience 1").Value -= 100;
                    stats.GetStat("PLevel 1").Value++;
                    UpdateStat();
                }
                break;
            case "P3":
                if (stats.GetStat("PExperience 2").Value >= 100)
                {
                    LevelUpEvent.Raise(this);
                    stats.GetStat("PExperience 2").Value -= 100;
                    stats.GetStat("PLevel 2").Value++;
                    UpdateStat();
                }
                break;
            case "P4":
                if (stats.GetStat("PExperience 3").Value >= 100)
                {
                    LevelUpEvent.Raise(this);
                    stats.GetStat("PExperience 3").Value -= 100;
                    stats.GetStat("PLevel 3").Value++;
                    UpdateStat();
                }
                break;
        }

    }

    private void UpdateStat()
    {
        switch (GetComponent<Transform>().tag)
        {
            case "P1":
                SetStats("");
                break;
            case "P2":
                SetStats(" 1");
                break;
            case "P3":
                SetStats(" 2");
                break;
            case "P4":
                SetStats(" 3");
                break;
        }
    }

    private void SetStats(string number)
    {
        Health = stats.GetStat("PHealth" + number).Value;
        Armor = stats.GetStat("PArmor" + number).Value;
        Level = stats.GetStat("PLevel" + number).Value;
        Speed = stats.GetStat("PSpeed" + number).Value;
        Damage = stats.GetStat("PDamage" + number).Value;
        EXP = stats.GetStat("PExperience" + number).Value;
    }


    /// <summary>
    ///     Entity Takes Damage
    ///     Reduce this incoming damage by our armor value
    /// </summary>
    /// <param name="damage">how much damage to take</param>
    public void TakeDamage(int damage)
    {
        var calculatedDamage = damage - Armor;

        var nexthealth = Health - calculatedDamage;

        var anim = GetComponent<Animator>();
        if (anim)
            anim.SetTrigger("Hurt");
        if (nexthealth <= 0)
        {
            Health = 0;
            Die();
        }
        else
        {
            Health = nexthealth;
        }
        UpdateStat();
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