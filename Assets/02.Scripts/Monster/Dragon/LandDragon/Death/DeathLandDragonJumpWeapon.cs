using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLandDragonJumpWeapon : WeaponCtrl
{
    [SerializeField]
    ParticleSystem[] particles;

    protected override void StartAttackCon()
    {
        base.StartAttackCon();
        foreach(var particle in particles)
        {
            particle.Play();
        }
    }
}
