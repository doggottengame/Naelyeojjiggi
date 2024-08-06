using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteWizardCtrl : MonsterCtrl
{
    [SerializeField]
    Transform magicPos;
    [SerializeField]
    GameObject magicPrefab;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(15000, 5000, 1200, 40, 0.7f);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }

    public override void StartAttack()
    {
        base.StartAttack();
        Instantiate(magicPrefab, magicPos.position, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
    }

    public override void StopAttack()
    {
        base.StopAttack();
    }
}
