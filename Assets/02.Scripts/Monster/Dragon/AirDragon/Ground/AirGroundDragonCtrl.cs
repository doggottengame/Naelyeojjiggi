using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGroundDragonCtrl : AirDragonCtrl
{
    [SerializeField]
    LayerMask layerMask;

    public override void MagicStart()
    {
        StartCoroutine(DeathMagic());
    }

    IEnumerator DeathMagic()
    {
        WaitForSeconds seconds1 = new WaitForSeconds(1.5f);
        WaitForSeconds seconds2 = new WaitForSeconds(0.5f);

        int count1 = 0;
        int count2 = 0;

        animator.SetBool(OnMagicAnimId, true);

        while (count1 < 3)
        {
            while (count2 < 5)
            {
                Vector3 magicPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(12, 28) + new Vector3(0, 10, 0);

                RaycastHit hit;

                if (Physics.Raycast(magicPos, Vector3.down, out hit, 20, layerMask))
                {
                    magicPos.y = hit.point.y + 0.05f;
                    Instantiate(magicPrefab, magicPos, Quaternion.identity).GetComponent<MagicCtrl>().Set(this, target);
                }
                else
                {
                    magicPos.y = transform.position.y + 0.05f;
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

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.BossKilled(0);
    }
}
