using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InTitle : MonoBehaviour
{
    bool onLoad;

    [SerializeField]
    ParticleSystem _particleSystem;
    [SerializeField]
    GameObject SlashPrefab;
    [SerializeField]
    Transform SlashPos;

    private void Awake()
    {
        GetComponent<Animator>().SetBool("Ground", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (onLoad) return;
        if (Input.anyKeyDown)
        {
            onLoad = true;
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void StartAttack()
    {
        _particleSystem.Play();
    }

    void EndOfAttackPosition()
    {
        Instantiate(SlashPrefab, SlashPos.position, SlashPos.rotation).GetComponent<SlashAura>().Set(100000, 100000);
    }

    void StopAttack()
    {
        SceneManager.LoadScene(1);
    }
}
