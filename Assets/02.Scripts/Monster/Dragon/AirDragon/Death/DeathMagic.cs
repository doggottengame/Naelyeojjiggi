using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMagic : DragonMagic
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
        dmgPer = 25;
        Set(magicCtrl.GetSelfCtrl().GetComponent<DragonCtrl>(), magicCtrl.GetTargetCtrl());
        Destroy(magicCtrl.gameObject, 4);
    }
}
