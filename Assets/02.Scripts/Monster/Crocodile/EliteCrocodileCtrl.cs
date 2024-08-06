using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteCrocodileCtrl : MonsterCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;

    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(2600 * (int)Mathf.Pow(worldNum, 2), 420 * (int)(worldNum * 1.5f), 100 + 400 * (int)(worldNum * 1.5f), 5 + 10 * worldNum, 1.5f);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
