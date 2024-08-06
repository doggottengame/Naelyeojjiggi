using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfChildCtrl : MonsterCtrl
{
    [SerializeField]
    bool elite;

    WolfCtrl groupBossCtrl;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(2000 + (elite ? 2000 : 0), 600 + (elite ? 100 : 0), 500 + (elite ? 100 : 0), elite ? 15 : 0, elite ? 0.5f : 1);
        farRange = 4;
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }

    public void SetBoss(WolfCtrl wolfCtrl)
    {
        groupBossCtrl = wolfCtrl;
    }

    protected override void UpdateCon2()
    {
        base.UpdateCon2();
        if (groupBossCtrl != null)
        {
            Transform mainTarget = groupBossCtrl.GetTargetTrans();
            if (mainTarget != null)
            {
                FoundPlayer(mainTarget);
            }
        }
        else
        {
            if (target != null)
            {
                groupBossCtrl.FoundPlayer(target);
            }
        }
    }
}
