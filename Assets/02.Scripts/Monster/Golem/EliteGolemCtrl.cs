using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGolemCtrl : MonsterCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;
    [SerializeField]
    WeaponCtrl dropWeaponCtrl;

    byte atkCount = 0;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(10000 + 5000 * worldNum, 400 + 500 * worldNum, 700 + 700 * worldNum, 20 + 10 * worldNum, 2);
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }

    public override void StartAttack()
    {
        if (atkCount == 0)
        {
            base.StartAttack();
        }
        else
        {
            dropWeaponCtrl.StartAttack();
        }
    }

    public override void StopAttack()
    {
        if (atkCount == 0)
        {
            base.StopAttack();
            atkCount++;
        }
        else
        {
            dropWeaponCtrl.StopAttack();
            atkCount = 0;
        }
    }
}
