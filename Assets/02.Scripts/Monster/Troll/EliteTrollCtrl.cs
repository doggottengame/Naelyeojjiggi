using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTrollCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(8000, 400, 800, 15, 1);
        regenBonus = health * 2 / 100;
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }
}
