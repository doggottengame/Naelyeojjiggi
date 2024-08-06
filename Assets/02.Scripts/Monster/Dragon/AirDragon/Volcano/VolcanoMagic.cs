using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoMagic : DragonMagic
{
    [SerializeField]
    ParticleSystem volcanos;

    [SerializeField]
    AudioSource HitSource, LavaSource;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 10;
        Set(magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>(), magicCtrl.GetTargetCtrl());
        Destroy(magicCtrl.gameObject, 4);

        yield return new WaitForSeconds(1.2f);
        HitSource.Play();
        LavaSource.Play();
    }

    protected override void MagicHit()
    {
        base.MagicHit();
        MagicOver();
    }

    protected override void MagicOver()
    {
        base.MagicOver();
        if (volcanos != null)
        {
            volcanos.transform.SetParent(null);
        }
    }
}
