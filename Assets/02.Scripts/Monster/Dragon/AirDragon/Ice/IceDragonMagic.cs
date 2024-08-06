using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonMagic : DragonMagic
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 25;
        Set(magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>(), magicCtrl.GetTargetCtrl());
    }

    protected override void MagicHit()
    {
        dragonCtrl.MagicHitToPlayer(dmgPer);
    }

    private void OnParticleSystemStopped()
    {
        dragonCtrl.MagicEnd();
        Destroy(magicCtrl.gameObject, 0.5f);
    }
}
