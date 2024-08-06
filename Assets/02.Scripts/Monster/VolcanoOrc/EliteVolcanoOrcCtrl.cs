using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteVolcanoOrcCtrl : MonsterCtrl
{
    [SerializeField]
    WeaponCtrl weaponCtrl2;
    bool attackLast;

    [SerializeField]
    GameObject magicPrefab;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(20000, 1000, 1800, 30, 3);
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }

    public override void TargetInAura()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.Hit(atkPower / 100, DmgType.Slow, 1);
        }
    }

    public override void StartAttack()
    {
        if (!attackLast)
        {
            weaponCtrl.StartAttack();
            Instantiate(magicPrefab, weaponCtrl.transform.position, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
        }
        else
        {
            weaponCtrl2.StartAttack();
            Instantiate(magicPrefab, weaponCtrl2.transform.position, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
        }
    }

    public override void StopAttack()
    {
        if (attackLast)
        {
            attackLast = true;
            weaponCtrl.StopAttack();
        }
        else
        {
            attackLast = false;
            onAttack = false;
            weaponCtrl2.StopAttack();
        }
    }
}
