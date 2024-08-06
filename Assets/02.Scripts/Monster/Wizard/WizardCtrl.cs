using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(12000, 3000, 900, 30, 1);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
