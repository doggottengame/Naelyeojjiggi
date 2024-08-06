using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMagic : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem _particleSystem;
    [SerializeField]
    protected MagicCtrl magicCtrl;
    protected DragonCtrl dragonCtrl;
    protected int dmgPer;

    public void Set(DragonCtrl dragonCtrl, Transform target)
    {
        this.dragonCtrl = dragonCtrl;
        _particleSystem.trigger.AddCollider(target.GetComponent<Collider>());
    }

    protected virtual void MagicOver()
    {

    }

    protected virtual void MagicHit()
    {
        if (dragonCtrl != null)
        {
            dragonCtrl.MagicHitToPlayer(dmgPer);
        }
    }

    private void OnParticleTrigger()
    {
        MagicHit();
    }
}
