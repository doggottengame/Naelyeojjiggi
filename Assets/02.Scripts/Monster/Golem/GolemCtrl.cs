using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemCtrl : MonsterCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(5000 + 4000 * worldNum, 200 + 400 * worldNum, 800 + 400 * worldNum, 10 + 10 * worldNum, 2);
        atkSpeed = 0.8f;
        base.Set(spawnCenter, spawnType);
    }
}
