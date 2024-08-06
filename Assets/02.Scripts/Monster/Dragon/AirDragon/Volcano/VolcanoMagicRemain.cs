using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoMagicRemain : DragonMagic
{
    bool inVolcano;

    float cnt;

    private void Update()
    {
        if (!inVolcano) return;

        if ((cnt += Time.deltaTime) > 0.1f)
        {
            dragonCtrl.TargetInFear(dmgPer);
            cnt = 0;
        }
    }

    protected override void MagicHit()
    {
        base.MagicHit();
        inVolcano = !inVolcano;
    }
}
