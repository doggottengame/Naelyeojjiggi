using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoOrcCtrl : MonsterCtrl
{
    [SerializeField]
    WeaponCtrl weaponCtrl2;
    bool attackLast;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(14000, 1000, 1200, 20, 2);
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }

    public override void TargetInAura()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.Hit(atkPower / 100, DmgType.Burn, 2);
        }
    }

    public override void StartAttack()
    {
        if (!attackLast)
        {
            weaponCtrl.StartAttack();
        }
        else
        {
            weaponCtrl2.StartAttack();
        }
    }

    public override void StopAttack()
    {
        if (attackLast)
        {
            attackLast = true;
            weaponCtrl.StopAttack();
        }
        else
        {
            attackLast = false;
            onAttack = false;
            weaponCtrl2.StopAttack();
        }
    }
}
