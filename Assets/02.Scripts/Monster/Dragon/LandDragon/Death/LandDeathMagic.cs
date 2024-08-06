using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDeathMagic : DragonMagic
{
    [SerializeField]
    AudioSource AudioSource;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 5;
        Set(magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>(), magicCtrl.GetTargetCtrl());

        yield return new WaitForSeconds(3);

        AudioSource.Play();
    }

    private void OnParticleSystemStopped()
    {
        dragonCtrl.MagicEnd();
        Destroy(magicCtrl.gameObject);
    }
}
