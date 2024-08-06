using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileWeapon : WeaponCtrl
{
    [SerializeField] Collider weaponCol2;

    protected override void StartAttackCon()
    {
        base.StartAttackCon();
        weaponCol2.enabled = true;
    }

    protected override void StopAttackCon()
    {
        base.StopAttackCon();
        weaponCol2.enabled = false;
    }
}
