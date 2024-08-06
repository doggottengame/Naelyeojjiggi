using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoOrcSpawnEff : SpawnEff
{
    [SerializeField]
    VolcanoOrcCtrl orcCtrl;
    [SerializeField]
    Transform orcTrans;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
    }

    private void Update()
    {
        if (orcTrans.localScale.x < 3)
        {
            orcTrans.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            orcTrans.localScale = Vector3.one * 3;
            orcCtrl.enabled = true;
            orcTrans.SetParent(spawnGroup);
            orcCtrl.Set(spawnCenter, spawnType);
            Destroy(gameObject);
        }
    }
}
