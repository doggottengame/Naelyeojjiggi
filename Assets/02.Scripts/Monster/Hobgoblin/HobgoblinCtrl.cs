using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobgoblinCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(400, 20, 30, 0, 1.5f);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
