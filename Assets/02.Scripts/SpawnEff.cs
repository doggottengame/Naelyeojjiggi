using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEff : MonoBehaviour
{
    protected Transform spawnCenter;
    protected Transform spawnGroup;
    protected byte spawnType;

    public virtual void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        this.spawnCenter = spawnCenter;
        this.spawnType = spawnType;
        this.spawnGroup = spawnGroup;
    }

    public virtual void EffectFinished()
    {

    }
}
