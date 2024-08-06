using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGroundMagic : DragonMagic
{
    CharaterCtrl target;

    bool inRange = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 50;
        dragonCtrl = magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out target) && !inRange)
        {
            Debug.Log("Start");
            inRange = true;
            StartCoroutine(DmgDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out target) && inRange)
        {
            Debug.Log("End");
            inRange = false;
            StopCoroutine(DmgDelay());
        }
    }

    IEnumerator DmgDelay()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.1f);

        while (inRange)
        {
            if (dragonCtrl != null && target != null)
            {
                target.FixedHitNoAction(dragonCtrl.atkPower / dmgPer, DmgType.Normal, 0);
            }

            yield return seconds;
        }
    }

    private void OnParticleSystemStopped()
    {
        dragonCtrl.MagicEnd();
        StopCoroutine(DmgDelay());
        Destroy(magicCtrl.gameObject);
    }
}
