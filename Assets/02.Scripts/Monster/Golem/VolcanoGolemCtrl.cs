using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoGolemCtrl : GolemCtrl
{
    public override void TargetInAura()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / 100, DmgType.Burn, 1);
        }
    }
}
