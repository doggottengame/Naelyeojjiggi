using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    protected CharaterCtrl target;

    [SerializeField]
    protected CharacterController characterController;

    bool HeadInWater;

    byte count = 0;
    float cnt = 0;

    private void Update()
    {
        if (!HeadInWater) return;

        if ((cnt += Time.deltaTime) < 0.5f) return;
        cnt = 0;

        WaterDmgPersist();
        if (count < 60)
        {
            count++;
        }
        else
        {
            WaterDmg();
        }
    }

    protected virtual void WaterDmgPersist()
    {

    }

    protected void WaterDmg()
    {
        target.FixedHitNoAction(10, DmgType.Normal, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInWater(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerOutWater(other.gameObject);
    }

    protected virtual void PlayerInWater(GameObject obj)
    {
        if (obj.CompareTag("Head") && !HeadInWater)
        {
            HeadInWater = true;
        }
    }

    protected virtual void PlayerOutWater(GameObject obj)
    {
        if (obj.CompareTag("Head") && HeadInWater)
        {
            count = 0;
            HeadInWater = false;
        }
    }
}
