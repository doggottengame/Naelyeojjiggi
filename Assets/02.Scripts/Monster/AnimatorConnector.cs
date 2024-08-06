using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorConnector : MonoBehaviour
{
    [SerializeField]
    CharaterCtrl charaterCtrl;

    [SerializeField]
    SpawnEff spawnEff;

    private void StartAttack()
    {
        charaterCtrl.StartAttack();
    }

    private void EndOfAttackPosition()
    {
        charaterCtrl.EndOfAttackPosition();
    }

    private void StopAttack()
    {
        charaterCtrl.StopAttack();
    }

    private void HitAnimFinished()
    {
        charaterCtrl.HitAnimFinished();
    }

    private void SpawnEffFinished()
    {
        if (spawnEff != null) spawnEff.EffectFinished();
    }
}
