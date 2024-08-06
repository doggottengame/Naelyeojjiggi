using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWater : Water
{
    protected override void WaterDmgPersist()
    {
        target.FixedHitNoAction(100, DmgType.Death, 2);
    }
}
