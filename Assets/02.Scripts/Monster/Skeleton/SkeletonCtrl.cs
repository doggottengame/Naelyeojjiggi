using UnityEngine;

public class SkeletonCtrl : MonsterCtrl
{
    public override void Set(Transform spawnCenter, byte spawnType)
    {
        Set(24000, 1200, 1500, 30, 1);
        atkSpeed = 1;
        base.Set(spawnCenter, spawnType);
    }
}
