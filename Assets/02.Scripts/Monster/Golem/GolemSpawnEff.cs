using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawnEff : SpawnEff
{
    [SerializeField]
    GolemCtrl golemCtrl;
    [SerializeField]
    Transform golemTrans;
    [SerializeField]
    Collider GroundingCollider;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
    }

    public override void EffectFinished()
    {
        base.EffectFinished();
        golemCtrl.enabled = true;
        golemTrans.SetParent(spawnGroup);
        golemCtrl.Set(spawnCenter, spawnType);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GroundingCollider.enabled = false;
            golemTrans.gameObject.SetActive(true);
        }
    }
}
