using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDragonFear : DragonFear
{
    protected override void Hit()
    {
        dragonCtrl.TargetInFear(100);
    }
}
