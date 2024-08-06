using System.Collections;
using UnityEngine;

public class DragonSpawn : SpawnEff
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    DragonCtrl dragonCtrl;
    [SerializeField]
    Transform dragonTrans;
    [SerializeField]
    Rigidbody dragonRb;

    bool animFinished;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        animator.SetBool("Land", false);
    }

    private void Update()
    {
        if (animFinished) return;
        if (dragonTrans.localPosition.y <= 2)
        {
            animator.SetBool("Land", true);
            animator.SetBool("Sleep", true);

            dragonTrans.SetParent(spawnGroup);

            dragonCtrl.enabled = true;
            dragonCtrl.Set(spawnCenter, spawnType);

            dragonRb.drag = 0;

            Destroy(gameObject);
        }
    }
}
