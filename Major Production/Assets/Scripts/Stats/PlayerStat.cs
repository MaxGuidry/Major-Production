using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStat : EntityStat
{
    public Stat ExpStat;

    private List<Stat> allStats;
    void Start()
    {
        
        ExpStat.BaseValue = 0;
        ArmorStat.BaseValue = 5;
        DamageStat.BaseValue = 10;

        ExpStat.ClearModifiers();
        ArmorStat.ClearModifiers();
        DamageStat.ClearModifiers();
    }

    /// <summary>
    /// Testing
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

    /// <summary>
    /// Add amount to the given Stats modifier list
    /// </summary>
    /// <param name="statToUpgrade"></param>
    /// <param name="amount"></param>
    public void StatUpgrade(Stat statToUpgrade, int amount)
    {
        statToUpgrade.AddModifier(amount);
    }

    /// <summary>
    /// Increase EXP once an enemy is killed
    /// </summary>
    /// <param name="expAmount"></param>
    public void OnEnemyKill(int expAmount)
    {
        StatUpgrade(ExpStat, expAmount);
    }

    /// <summary>
    /// Add Modifier to Armor Stat
    /// </summary>
    /// <param name="amount"></param>
    public void ArmorStatUpgrade(int amount)
    {
        StatUpgrade(ArmorStat, amount);
        Debug.Log(name + " Armor Mod Added: " + amount + " Total: " + ArmorStat.GetValue());
    }
}
