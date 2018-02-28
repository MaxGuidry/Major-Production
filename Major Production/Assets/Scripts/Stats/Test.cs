using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Test : ScriptableObject
{
    public PlayerStat player;
    public void TestArmorUpgrade(int amount)
    {
        player.ArmorStatUpgrade(amount);
    }
}
