using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteWizardSpawnEff : SpawnEff
{
    EliteWizardCtrl wizardCtrl;
    Transform wizardTrans;
    Vector3 targetPos = new Vector3(0, 0.3f, 0);
    bool effStart;

    public override void Set(Transform spawnCenter, byte spawnType, Transform spawnGroup)
    {
        base.Set(spawnCenter, spawnType, spawnGroup);
        wizardCtrl = transform.GetComponentInChildren<EliteWizardCtrl>();
        wizardTrans = wizardCtrl.transform;

        effStart = true;
    }

    private void Update()
    {
        if (!effStart) return;

        wizardTrans.localPosition = Vector3.Lerp(wizardTrans.localPosition, targetPos, Time.deltaTime);

        if ((wizardTrans.localPosition - targetPos).sqrMagnitude <= 0.0025f)
        {
            wizardTrans.localPosition = targetPos;
            wizardTrans.SetParent(spawnGroup);
            wizardCtrl.Set(spawnCenter, spawnType);
            Destroy(gameObject);
        }
    }
}
