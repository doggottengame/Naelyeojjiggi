using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteVolcanoMagic : MagicCtrl
{
    float cnt = 5;
    Vector3 offset = new Vector3(0, 0.3f, 0);

    // Update is called once per frame
    void Update()
    {
        if (target == null || (cnt -= Time.deltaTime) <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime);
        transform.LookAt(target.position);

        if ((target.position - transform.position).sqrMagnitude < 0.0025)
        {
            target.GetComponent<CharaterCtrl>().Hit(GetSelfCtrl().atkPower, DmgType.Burn, 5);
            Destroy(gameObject);
        }
    }
}