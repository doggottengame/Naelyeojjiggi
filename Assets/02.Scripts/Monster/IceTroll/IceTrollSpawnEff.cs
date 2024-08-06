using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrollSpawnEff : SpawnEff
{
    IceTrollCtrl trollCtrl;
    Transform trollTrans;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        trollCtrl = transform.GetComponentInChildren<IceTrollCtrl>();
        trollTrans = trollCtrl.transform;
        StartCoroutine(Set());
    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(1);

        trollTrans.SetParent(spawnGroup);
        trollCtrl.Set(spawnCenter, spawnType);
        Destroy(gameObject);
    }
}
