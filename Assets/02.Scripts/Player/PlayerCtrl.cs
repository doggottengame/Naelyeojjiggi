using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerCtrl : CharaterCtrl
{
    [SerializeField]
    DataCtrl dataCtrl;

    CharacterController characterController;

    Transform camTrans;

    [SerializeField]
    GameObject icedEff;

    private int SpawnAnimId = Animator.StringToHash("Spawn");

    bool inWater;

    private void Awake()
    {
        Set(50 + 50 * dataCtrl.lv + BossCal() * 10000 / 3, 10 + (int)Mathf.Pow(dataCtrl.lv, 4f / 3f) + BossCal() * 2000 / 3, dataCtrl.lv / 20 + BossCal() * 200, (int)Mathf.Clamp(dataCtrl.kill / 100 + BossCal() * 5, 0, 99), -0.1f);
        atkSpeed = Mathf.Clamp(1 + (float)dataCtrl.weaponLv / 1000, 1, 3);
        characterController = GetComponent<CharacterController>();

        camTrans = Camera.main.transform;

        HpBarCtrl();
        ExpBarCtrl();
        KillRecordCtrl();
    }

    int BossCal()
    {
        return (dataCtrl.GroundDragon ? 1 : 0) + (dataCtrl.IceDragon ? 2 : 0) + (dataCtrl.VolcanoDragon ? 3 : 0) + (dataCtrl.DeathDragon ? 4 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCon();
        StateCheck();
        ClickMovement();
        AudioCtrl();
        UIUpdate();
        icedEff.SetActive(iced);
    }

    void StateCheck()
    {
        if (!iced && slowCnt >= 5)
        {
            icedCnt = 3;
            slowCnt = 0;
        }
        slowCnt = Mathf.Clamp(slowCnt - Time.deltaTime, 0, float.MaxValue);
        icedCnt = Mathf.Clamp(icedCnt - Time.deltaTime, 0, float.MaxValue);
        if (DeathStack >= 100)
        {
            Hit(int.MaxValue, DmgType.Normal, 0);

            DeathStack = 0;
            deathCnt = 1;
        }
        if (DeathStack > 0 && (deathCnt -= Time.deltaTime) <= 0)
        {
            StateSource.Stop();
            StateSource.PlayOneShot(deathClip);
            FixedHitNoAction(100, DmgType.Normal, 0);
            DeathStack--;
            deathCnt = 1;
        }
        if (BurnStack > 0 && (burnCnt -= Time.deltaTime) <= 0)
        {
            StateSource.Stop();
            StateSource.PlayOneShot(burnClip);
            FixedHitNoAction(BurnStack * 20, DmgType.Normal, 0);
            BurnStack--;
            burnCnt = 1;
        }
    }

    protected override void Hit()
    {
        rolling = false;
        HpBarCtrl();
        base.Hit();
    }

    public void MonsterKilled(int exp)
    {
        dataCtrl.kill++;
        int lv = dataCtrl.lv;
        int expTmp = dataCtrl.exp + exp;

        for (; expTmp > 10 + (int)Mathf.Pow(lv, 4f / 3f); lv++)
        {
            expTmp -= 10 + (int)Mathf.Pow(lv, 4f / 3f);
            LvUp();
        }
        dataCtrl.exp = expTmp;

        GameManager.Instance.SaveData();
        ExpBarCtrl();
        KillRecordCtrl();
    }

    public override void EndOfAttackPosition()
    {
        base.EndOfAttackPosition();
        ActiveSource.Stop();
        ActiveSource.clip = attackClip;
        ActiveSource.Play();
        dataCtrl.weaponLv++;
        atkSpeed = Mathf.Clamp(1 + (float)dataCtrl.weaponLv / 1000, 1, 3);
        GameManager.Instance.SaveData();
    }

    void LvUp()
    {
        dataCtrl.lv++;
        StatusUpdate();
        nowHealth = health;
        Heal();
    }

    public void StatusUpdate()
    {
        health = 50 + 50 * dataCtrl.lv + BossCal() * 10000 / 3;
        atkPower = 10 + (int)Mathf.Pow(dataCtrl.lv, 4f / 3f) + BossCal() * 2000 / 3;
        def = dataCtrl.lv / 20 + BossCal() * 200;
        dec = (int)Mathf.Clamp(dataCtrl.kill / 100 + BossCal() * 5, 0, 99);
    }

    protected override void Heal()
    {
        HpBarCtrl();
    }

    protected override void HitCon(DmgType dmgType, float addTime)
    {
        base.HitCon(dmgType, addTime);
        switch (dmgType)
        {
            case DmgType.Slow:
                slowCnt += addTime;
                lastSlowCnt = slowCnt;
                break;

            case DmgType.Burn:
                StateSource.Stop();
                StateSource.PlayOneShot(burnClip);
                BurnStack += (int)addTime;
                burnCnt = 1;
                FixedHitNoAction(BurnStack * 20, DmgType.Normal, 0);
                break;

            case DmgType.Death:
                StateSource.Stop();
                StateSource.PlayOneShot(deathClip);
                DeathStack += (int)addTime;
                deathCnt = 1;
                break;
        }
    }

    public void SetDataCtrl(DataCtrl dataCtrl)
    {
        this. dataCtrl = dataCtrl;
    }

    public DataCtrl GetDataCtrl()
    {
        return dataCtrl;
    }

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.ChangeArea(0);
        AudioSource.Stop();
        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(5);

        slowCnt = 0;
        icedCnt = 0;
        burnCnt = 0;
        BurnStack = 0;
        deathCnt = 0;
        DeathStack = 0;

        nowHealth = health;
        StopAttack();

        transform.position = new Vector3(0, 12, -8);
        transform.rotation = Quaternion.identity;

        animator.SetTrigger(SpawnAnimId);
        HpBarCtrl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && other.GetComponent<EnemySearcher_ByMonster>().GetMonsterCtrl().IsBoss)
        {
            targetBossCharacterCtrl = other.GetComponent<EnemySearcher_ByMonster>().GetMonsterCtrl();
        }
        else if (other.gameObject.layer == 4)
        {
            if (!other.gameObject.CompareTag("Volcano"))
            {
                inWater = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && other.GetComponent<EnemySearcher_ByMonster>().GetMonsterCtrl().IsBoss)
        {
            targetBossCharacterCtrl = null;
        }
        else if (other.gameObject.layer == 4)
        {
            if (!other.gameObject.CompareTag("Volcano"))
            {
                inWater = false;
            }
        }
    }
}
