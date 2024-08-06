using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTrollWeaponCtrl : WeaponCtrl
{
    [SerializeField]
    GameObject dustPrefab;
    [SerializeField]
    Transform dustTrans;
    [SerializeField]
    LayerMask layerMask;

    protected override void EndOfAttackPositionCon()
    {
        base.EndOfAttackPositionCon();
        RaycastHit hit;
        if (Physics.Raycast(dustTrans.position, Vector3.down, out hit, 1.5f, layerMask))
        {
            GameObject dustTmp = Instantiate(dustPrefab, hit.point + new Vector3(0, 0.3f, 0), Quaternion.Euler(hit.normal));
            dustTmp.GetComponent<DustAura>().Set(100, atkPower);
        }
    }

    protected override void StopAttackCon()
    {
        base.StopAttackCon();
    }
}
