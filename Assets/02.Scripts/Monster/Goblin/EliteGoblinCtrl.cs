using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGoblinCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(600, 50, 50, 10, 0.5f);
        atkSpeed = 1.5f;
        base.Set(spawnCenter, spawnType);
    }
}
