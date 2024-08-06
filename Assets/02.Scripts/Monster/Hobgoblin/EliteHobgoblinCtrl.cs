using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteHobgoblinCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(800, 80, 100, 10, 1);
        atkSpeed = 2;
        base.Set(spawnCenter, spawnType);
    }
}
