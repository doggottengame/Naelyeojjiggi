using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFear : MonoBehaviour
{
    [SerializeField]
    protected DragonCtrl dragonCtrl;
    protected CharaterCtrl target;

    bool inRange = false;

    float cnt = -0.1f;

    private void Update()
    {
        if (inRange)
        {
            if ((cnt -= Time.deltaTime) <= 0)
            {
                Hit();
                cnt = 0.1f;
            }
        }
        else
        {
            cnt = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    protected virtual void Hit()
    {
        dragonCtrl.TargetInFear(50);
    }
}
