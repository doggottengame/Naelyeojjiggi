using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobgoblinSpawnEff : SpawnEff
{
    HobgoblinCtrl hobgoblinCtrl;
    Transform hobgoblinTrans;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        hobgoblinCtrl = transform.GetComponentInChildren<HobgoblinCtrl>();
        hobgoblinTrans = hobgoblinCtrl.transform;

        StartCoroutine(Set());

    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(1);

        hobgoblinTrans.SetParent(spawnGroup);
        hobgoblinCtrl.Set(spawnCenter, spawnType);
        Destroy(gameObject);
    }
}
