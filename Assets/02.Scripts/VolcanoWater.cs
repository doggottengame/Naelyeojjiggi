using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoWater : Water
{
    protected override void WaterDmgPersist()
    {
        target.FixedHitNoAction(100, DmgType.Burn, 2);
    }
}