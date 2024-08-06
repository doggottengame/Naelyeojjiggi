using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    [SerializeField]
    DragonCtrl dragonCtrl;

    private void OnParticleTrigger()
    {
        HitToTarget();
    }

    protected virtual void HitToTarget()
    {
        dragonCtrl.BreathHitToPlayer();
    }
}
