using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileSpawn : SpawnEff
{
    CrocodileCtrl crocodileCtrl;
    Transform crocodileTrans;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        crocodileCtrl = transform.GetComponentInChildren<CrocodileCtrl>();
        crocodileTrans = crocodileCtrl.transform;

        StartCoroutine(Set());
    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(1);

        crocodileTrans.SetParent(spawnGroup);
        crocodileCtrl.Set(spawnCenter, spawnType);
        Destroy(gameObject);
    }
}
