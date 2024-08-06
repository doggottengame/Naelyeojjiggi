using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCenter : MonoBehaviour
{
    [SerializeField]
    Transform spawnGroup;
    [SerializeField]
    [Range(1, 20)] protected byte Range = 10;
    [SerializeField]
    protected byte spawnType, targetMass;
    protected byte mass;
    [SerializeField]
    protected GameObject spawnPrefab, eliteSpawnPrefab;

    protected bool IsBossSpawner;
    protected bool onSpawning;

    private void Awake()
    {
        Set();
    }

    private void Update()
    {
        UpdateCon();
    }

    protected void Set()
    {
        StartCoroutine(SpawnDelay(1));
    }

    protected void UpdateCon()
    {
        if (mass < targetMass)
        {
            StartCoroutine(SpawnDelay(5));
        }
    }

    IEnumerator SpawnDelay(int delayTime)
    {
        if (!onSpawning)
        {
            onSpawning = true;
            yield return new WaitForSeconds(delayTime);

            Vector3 spawnRandPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(1.5f, Range);

            if (Random.Range(0, 10) < 9)
            {
                Instantiate(spawnPrefab, transform.position + spawnRandPos, Quaternion.Euler(0, Random.Range(-180, 180), 0)).GetComponent<SpawnEff>().Set(transform, spawnType, spawnGroup);
            }
            else
            {
                Instantiate(eliteSpawnPrefab, transform.position + spawnRandPos, Quaternion.Euler(0, Random.Range(-180, 180), 0)).GetComponent<SpawnEff>().Set(transform, spawnType, spawnGroup);
            }

            onSpawning = false;

            mass++;

            if (mass < targetMass)
            {
                StartCoroutine(SpawnDelay(delayTime));
            }
        }
    }

    public void MonsterKilled()
    {
        if (mass > 0) mass--;
    }
}
