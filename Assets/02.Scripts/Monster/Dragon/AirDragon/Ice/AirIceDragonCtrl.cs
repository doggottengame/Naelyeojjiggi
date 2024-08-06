using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirIceDragonCtrl : AirDragonCtrl
{
    [SerializeField]
    LayerMask layerMask;

    public override void MagicStart()
    {
        animator.SetBool(OnMagicAnimId, true);

        Vector3 magicPos = transform.position + new Vector3(0, 10, 0);

        RaycastHit hit;

        if (Physics.Raycast(magicPos, Vector3.down, out hit, 20, layerMask))
        {
            magicPos.y = hit.point.y + 0.05f;
            Instantiate(magicPrefab, magicPos, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
        }
        else
        {
            magicPos.y = transform.position.y + 0.05f;
            Instantiate(magicPrefab, magicPos, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
        }
    }

    public override void BreathHitToPlayer()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / 50, DmgType.Slow, 0.5f);
        }
    }

    public override void MagicHitToPlayer(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.Hit(atkPower / dmg, DmgType.Slow, 6);
        }
    }

    public override void MagicEnd()
    {
        base.MagicEnd();

        animator.SetBool(OnMagicAnimId, false);
    }

    public override void TargetInFear(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / dmg, DmgType.Slow, 0.25f);
        }
    }

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.BossKilled(1);
    }
}
