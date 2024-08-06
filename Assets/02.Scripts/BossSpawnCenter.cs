using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnCenter : MonoBehaviour
{
    [SerializeField]
    Transform spawnGroup;
    [SerializeField]
    byte spawnType;
    [SerializeField]
    GameObject spawnPrefab;

    bool bossSpawned;
    float cnt;

    protected void Awake()
    {
        Instantiate(spawnPrefab, transform.position, transform.rotation).GetComponent<SpawnEff>().Set(transform, spawnType, spawnGroup);
        bossSpawned = true;
    }

    protected void Update()
    {
        if (bossSpawned || (cnt -= Time.deltaTime) >= 0) return;
        Instantiate(spawnPrefab, transform.position, transform.rotation).GetComponent<SpawnEff>().Set(transform, spawnType, spawnGroup);
        bossSpawned = true;
    }

    public void BossKilled()
    {
        cnt = 30;
        bossSpawned = false;
    }
}
