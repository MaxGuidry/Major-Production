using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStat : EntityStat
{
    public int EXP
    {
        get { return stats["EXP"].Value; }
        set { stats["EXP"].Value = value; }
    }

    /// <summary>
    /// Testing
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

}
