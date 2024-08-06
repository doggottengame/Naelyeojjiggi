using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVolcanoDragonCtrl : AirDragonCtrl
{
    [SerializeField]
    LayerMask layerMask;

    public override void MagicStart()
    {
        StartCoroutine(VolcanoMagic());
    }

    IEnumerator VolcanoMagic()
    {
        WaitForSeconds seconds1 = new WaitForSeconds(1);
        WaitForSeconds seconds2 = new WaitForSeconds(0.1f);

        int count1 = 0;
        int count2 = 0;

        animator.SetBool(OnMagicAnimId, true);

        while (count1 < 3)
        {
            while (count2 < 5)
            {
                Vector3 magicPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(12, 28);
                magicPos.y = 10;

                RaycastHit hit;

                if (Physics.Raycast(magicPos, Vector3.down, out hit, 20, layerMask))
                {
                    magicPos.y = hit.point.y + 0.1f;
                    Instantiate(magicPrefab, magicPos, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
                }
                else
                {
                    magicPos.y = 0.1f;
                    Instantiate(magicPrefab, magicPos, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
                }

                count2++;
                yield return seconds2;
            }
            count2 = 0;
            count1++;
            yield return seconds1;
        }

        animator.SetBool(OnMagicAnimId, false);
    }

    public override void TargetInFear(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / dmg, DmgType.Burn, 1);
        }
    }

    public override void BreathHitToPlayer()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHit(atkPower / 50, DmgType.Burn, 1);
        }
    }

    public override void MagicHitToPlayer(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / dmg, DmgType.Burn, 5);
        }
    }

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.BossKilled(2);
    }
}
