using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLandDragonDashWeapon : WeaponCtrl
{
    LandDragonCtrl landDragonCtrl;

    private void Awake()
    {
        landDragonCtrl = charaterCtrl.GetComponent<LandDragonCtrl>();
    }

    protected override void StartAttackCon()
    {
        landDragonCtrl.DashStart();
    }

    protected override void StopAttackCon()
    {
        landDragonCtrl.DashStop();
    }
}
