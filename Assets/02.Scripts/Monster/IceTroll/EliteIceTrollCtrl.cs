using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteIceTrollCtrl : MonsterCtrl
{
    byte attackCnt = 0;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(18000, 1400, 1200, 30, 2);
        regenBonus = health * 4 / 100;
        atkSpeed = 0.6f;
        base.Set(spawnCenter, spawnType);
    }

    public override void TargetInAura()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.Hit(atkPower / 100, DmgType.Slow, 2);
        }
    }

    public override void StartAttack()
    {
        weaponCtrl.StartAttack();
    }

    public override void EndOfAttackPosition()
    {
        weaponCtrl.EndOfAttackPosition();
    }

    public override void StopAttack()
    {
        if (attackCnt < 2)
        {
            attackCnt++;
        }
        else
        {
            attackCnt = 0;
            onAttack = false;
        }
        weaponCtrl.StopAttack();
    }
}
