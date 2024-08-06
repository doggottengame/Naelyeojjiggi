using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGoblinSpawnEff : SpawnEff
{
    EliteGoblinCtrl goblinCtrl;
    Transform goblinTrans;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        goblinCtrl = transform.GetComponentInChildren<EliteGoblinCtrl>();
        goblinTrans = goblinCtrl.transform;
        StartCoroutine(Set());
    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(1);

        goblinTrans.SetParent(spawnGroup);
        goblinCtrl.Set(spawnCenter, spawnType);
        Destroy(gameObject);
    }
}
