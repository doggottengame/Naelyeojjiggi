using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawnEff : SpawnEff
{
    SkeletonCtrl skeletonCtrl;
    Transform skeletonTrans;
    Vector3 targetPos = new Vector3(0, 0.3f, 0);
    bool effStart;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        skeletonCtrl = transform.GetComponentInChildren<SkeletonCtrl>();
        skeletonTrans = skeletonCtrl.transform;
        effStart = true;
    }

    private void Update()
    {
        if (!effStart) return;

        skeletonTrans.localPosition = Vector3.Lerp(skeletonTrans.localPosition, targetPos, Time.deltaTime);

        if ((skeletonTrans.localPosition - targetPos).sqrMagnitude <= 0.0025f)
        {
            skeletonTrans.localPosition = targetPos;
            skeletonTrans.SetParent(spawnGroup);
            skeletonCtrl.Set(spawnCenter, spawnType);
            Destroy(gameObject);
        }
    }
}
