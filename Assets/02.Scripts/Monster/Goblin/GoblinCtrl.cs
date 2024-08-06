using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(50, 5, 0, 0, 1);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
