using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteSkeletonCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(30000, 1500, 2000, 40, 1);
        atkSpeed = 2;
        base.Set(spawnCenter, spawnType);
    }

    public override void TargetInAura()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / 100, DmgType.Death, 1);
        }
    }
}
