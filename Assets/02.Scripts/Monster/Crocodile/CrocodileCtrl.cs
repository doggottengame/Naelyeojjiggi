using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileCtrl : MonsterCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(2000 * (int)Mathf.Pow(worldNum, 2), 300 * (int)(worldNum * 1.5f), 100 + 200 * (int)(worldNum * 1.5f), 5, 1.5f);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
