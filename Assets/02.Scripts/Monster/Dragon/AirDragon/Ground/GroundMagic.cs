using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMagic : DragonMagic
{
    [SerializeField]
    ParticleSystem stones;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 10;
        Set(magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>(), magicCtrl.GetTargetCtrl());
        Destroy(magicCtrl.gameObject, 4);
    }

    protected override void MagicHit()
    {
        base.MagicHit();
        MagicOver();
    }

    protected override void MagicOver()
    {
        base.MagicOver();
        if (stones != null)
        {
            stones.transform.SetParent(null);
        }
        Destroy(stones.gameObject, 1);
    }
}
