using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDragonSpawn : SpawnEff
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    DragonCtrl dragonCtrl;
    [SerializeField]
    Transform dragonTrans;
    [SerializeField]
    Rigidbody dragonRb;
    Vector3 targetPos = new Vector3(0, 0.1f, 0);

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
    }

    private void Update()
    {
        dragonTrans.localPosition = Vector3.Lerp(dragonTrans.localPosition, targetPos, Time.deltaTime);

        if ((targetPos - dragonTrans.localPosition).sqrMagnitude < 0.09f)
        {
            dragonTrans.localPosition = targetPos;
            dragonTrans.SetParent(spawnGroup);
            dragonCtrl.enabled = true;
            dragonCtrl.Set(spawnCenter, spawnType);
            dragonRb.constraints = RigidbodyConstraints.FreezeRotation;

            Destroy(gameObject);
        }
    }
}
