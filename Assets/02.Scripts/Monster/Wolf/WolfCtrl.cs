using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCtrl : MonsterCtrl
{
    [SerializeField]
    bool elite;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(3000 + (elite ? 3000 : 1000), 1000 + (elite ? 100 : 0), 700 + (elite ? 300 : 100), elite ? 25 : 10, elite ? 0.5f : 1);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }

    public Transform GetTargetTrans()
    {
        return target;
    }
}
