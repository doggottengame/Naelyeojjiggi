using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(4000, 200, 600, 5, 1.5f);
        regenBonus = health * 1 / 100;
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }
}
