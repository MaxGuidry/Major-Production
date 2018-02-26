using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : EntityStat
{
    public Stat ExpStat;

    void Start()
    {
        ExpStat.BaseValue = 0;
        ArmorStat.BaseValue = 5;
        DamageStat.BaseValue = 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

    public void OnEnemyKill(int expAmount)
    {
        ExpStat.AddModifier(expAmount);
    }

    public void StatUpgrade(Stat statToUpgrade, int amount)
    {
        statToUpgrade.AddModifier(amount);
    }

    public void ArmorStatUpgrade(int amount)
    {
        StatUpgrade(ArmorStat, amount);
        Debug.Log(name + " Armor Mod Added: " + amount + " Total: " + ArmorStat.GetValue());
    }
}
