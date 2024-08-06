using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawnEff : SpawnEff
{
    WolfGroup wolfGroup;
    bool effStart;
    float cnt = 0;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        wolfGroup = GetComponent<WolfGroup>();

        effStart = true;
    }

    private void Update()
    {
        if (!effStart) return;

        if ((cnt += Time.deltaTime) >= 1.5f)
        {
            wolfGroup.enabled = true;
            wolfGroup.Set(spawnCenter, spawnType);
            transform.SetParent(spawnGroup);
            Destroy(this);
        }
    }
}
