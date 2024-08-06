using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGroundDragonCtrl : LandDragonCtrl
{
    public override void MagicStart()
    {
        animator.SetBool(OnMagicAnimId, true);
        Instantiate(magicPrefab, transform.position, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
    }

    public override void MagicEnd()
    {
        base.MagicEnd();

        animator.SetBool(OnMagicAnimId, false);
    }
}
