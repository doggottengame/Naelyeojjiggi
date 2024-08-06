using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearcher_ByMonster : MonoBehaviour
{
    [SerializeField]
    MonsterCtrl monsterCtrl;

    public MonsterCtrl GetMonsterCtrl()
    {
        return monsterCtrl;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Head"))
        {
            monsterCtrl.FoundPlayer(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Head") && monsterCtrl != null)
        {
            monsterCtrl.MissPlayer();
        }
    }
}
