using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagicCtrl : MagicCtrl
{
    CharaterCtrl targetCtrl;

    bool inRange = false;

    float cnt;

    private IEnumerator Start()
    {
        while (transform.localScale.x < 1)
        {
            transform.localScale += 0.1f * Vector3.one;

            yield return new WaitForSeconds(0.01f);
        }
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (inRange && (cnt += Time.deltaTime) >= 0.1f)
        {
            cnt = 0;
            if (targetCtrl != null)
            {
                targetCtrl.FixedHitNoAction(charaterCtrl.atkPower / 200, DmgType.Slow, 0.2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out targetCtrl) && !inRange)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out targetCtrl) && inRange)
        {
            inRange = false;
        }
    }
}
