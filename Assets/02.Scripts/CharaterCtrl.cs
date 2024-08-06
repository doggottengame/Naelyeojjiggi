using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum DmgType
{
    Normal,
    Slow,
    Burn,
    Death
}

public class CharaterCtrl : MonoBehaviour
{
    protected SpawnCenter spawnCenter;
    protected BossSpawnCenter bossSpawnCenter;

    [SerializeField] protected WeaponCtrl weaponCtrl;
    [SerializeField] protected GroundChecker groundChecker;

    [SerializeField] protected Animator animator;
    protected int MoveAnimId = Animator.StringToHash("Move");
    //private int JumpAnimId = Animator.StringToHash("Jump");
    protected int RollAnimId = Animator.StringToHash("Roll");
    private int GroundAnimId = Animator.StringToHash("Ground");
    protected int AttackAnimId = Animator.StringToHash("Attack");
    protected int AtkSpeedAnimId = Animator.StringToHash("AtkSpeed");
    private int HitAnimId = Animator.StringToHash("Hit");
    private int DeathAnimId = Animator.StringToHash("Death");
    public int health;
    protected int nowHealth;
    protected int regenBonus;
    protected int regenHealth => Mathf.Clamp(health / 1000 + regenBonus, 1, health);
    public int atkPower;
    protected int def;
    protected int dec;
    public float atkCool;
    protected float atkCoolCnt;
    public float atkSpeed;
    protected float verSpeed, horSpeed;
    protected float lastSlowCnt;
    protected float slowCnt;
    protected float icedCnt;
    protected float burnCnt;
    protected float deathCnt;
    protected float stunCnt;

    protected int MainSpeed => slowCnt <= 0 ? 2 : (iced ? 0 : 1);
    protected int BurnStack;
    protected int DeathStack;

    protected bool ground;
    protected bool hitted;
    protected bool onAttack;
    protected bool iced => icedCnt > 0;
    protected bool stuned;
    protected bool onCC => iced || stuned;
    protected bool ccImune;
    public bool dmgImune = true;
    protected bool canMove => !dead && !(!dmgImune && hitted) && !(!ccImune && onCC);

    protected bool dead = false;

    [SerializeField]
    GameObject massShowerPrefab;

    [SerializeField]
    protected AudioSource ActiveSource;
    [SerializeField]
    protected AudioClip swingClip;
    [SerializeField]
    protected AudioClip deadClip, hitClip;

    /// <summary>
    /// 값들을 설정하기 위해 Awake에서 호출
    /// </summary>
    /// <param name="health">체력</param>
    protected void Set(int health, int atkPower, int def, int dec, float atkCool)
    {
        this.atkCool = atkCool;
        atkCoolCnt = -0.1f;

        this.health = health;
        nowHealth = health;

        this.atkPower = atkPower;
        this.def = def;
        this.dec = dec;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetFloat(AtkSpeedAnimId, atkSpeed * MainSpeed / 2);
        dmgImune = false;
        StartCoroutine(HealDelay());
    }
    /// <summary>
    /// 자식 클래스의 Update와 연결
    /// </summary>
    protected void UpdateCon()
    {
        atkCoolCnt = Mathf.Clamp(atkCoolCnt - Time.deltaTime, -0.1f, atkCool);
        animator.SetBool(GroundAnimId, ground);
    }

    IEnumerator HealDelay()
    {
        WaitForSeconds seconds = new WaitForSeconds(1);

        while(true)
        {
            if (!dead)
            {
                nowHealth = Mathf.Clamp(nowHealth + regenHealth, 0, health);

                Heal();
            }

            yield return seconds;
        }
    }

    protected virtual void Move()
    {
        animator.SetBool(MoveAnimId, true);
    }
    protected virtual void Stop()
    {
        animator.SetBool(MoveAnimId, false);
    }
    protected void Jump()
    {

    }
    protected void Ground()
    {
        animator.SetTrigger(GroundAnimId);
    }
    protected void Attack()
    {
        if (canMove && !onAttack && atkCoolCnt <= 0)
        {
            onAttack = true;
            animator.SetFloat(AtkSpeedAnimId, atkSpeed * MainSpeed / 2);
            animator.SetTrigger(AttackAnimId);
        }
    }
    protected virtual void HitCon(DmgType dmgType, float addTime)
    {

    }
    public int Hit(int dmg, DmgType dmgType, float addTime)
    {
        HitCon(dmgType, addTime);
        if (dead || dmgImune) return 0;
        int finDmg = Mathf.Clamp(dmg * (100 - dec) / 100 - def, int.MinValue, int.MaxValue);
        return DmgCal(finDmg);
    }
    public int HitNoAction(int dmg, DmgType dmgType, float addTime)
    {
        HitCon(dmgType, addTime);
        if (dead || dmgImune) return 0;
        int finDmg = Mathf.Clamp(dmg * (100 - dec) / 100 - def, int.MinValue, int.MaxValue);
        return DmgNoActionCal(finDmg);
    }
    public int FixedHit(int dmg, DmgType dmgType, float addTime)
    {
        HitCon(dmgType, addTime);
        if (dead || dmgImune) return 0;
        return DmgCal(dmg);
    }
    public int FixedHitNoAction(int dmg, DmgType dmgType, float addTime)
    {
        HitCon(dmgType, addTime);
        if (dead || dmgImune) return 0;
        return DmgNoActionCal(dmg);
    }
    protected virtual int DmgCal(int dmg)
    {
        Vector3 offset = new Vector3(0, 1, 0);
        MassShower massShower = Instantiate(massShowerPrefab, transform.position + offset, Quaternion.identity).GetComponent<MassShower>();
        nowHealth = Mathf.Clamp(nowHealth - dmg, 0, health);

        Hit();

        if (nowHealth <= 0)
        {
            massShower.Set(dmg, TextType.Damage);
            dead = true;
            Death();
            return dmg;
        }

        if (!dead && !ccImune)
        {
            ActiveSource.PlayOneShot(hitClip);

            hitted = true;

            animator.SetTrigger(HitAnimId);
        }

        if (dmg > 0)
        {
            massShower.Set(dmg, TextType.Damage);
        }
        else if (dmg < 0)
        {
            massShower.Set(-dmg, TextType.Heal);
        }

        return dmg;
    }
    protected virtual int DmgNoActionCal(int dmg)
    {
        Vector3 offset = new Vector3(0, 1, 0);
        MassShower massShower = Instantiate(massShowerPrefab, transform.position + offset, Quaternion.identity).GetComponent<MassShower>();
        nowHealth = Mathf.Clamp(nowHealth - dmg, 0, health);

        Hit();

        if (nowHealth <= 0)
        {
            massShower.Set(dmg, TextType.Damage);
            dead = true;
            Death();
            return dmg;
        }

        if (dmg > 0)
        {
            massShower.Set(dmg, TextType.Damage);
        }
        else if (dmg < 0)
        {
            massShower.Set(-dmg, TextType.Heal);
        }

        return dmg;
    }
    protected virtual void Death()
    {
        animator.SetBool(MoveAnimId, false);
        ActiveSource.Stop();
        ActiveSource.clip = deadClip;
        ActiveSource.Play();
        animator.SetTrigger(DeathAnimId);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    protected virtual void Heal()
    {

    }
    protected virtual void Hit()
    {
        if (hitClip != null)
        {
            ActiveSource.Stop();
            ActiveSource.volume = 0.5f;
            ActiveSource.clip = hitClip;
            ActiveSource.Play();
        }
        StopAttack();
    }

    public virtual void StartAttack()
    {
        ActiveSource.Stop();
        ActiveSource.volume = 1;
        ActiveSource.clip = swingClip;
        ActiveSource.Play();
        weaponCtrl.StartAttack();
    }

    public virtual void EndOfAttackPosition()
    {
        weaponCtrl.EndOfAttackPosition();
    }

    public virtual void StopAttack()
    {
        atkCoolCnt = atkCool;
        onAttack = false;
        weaponCtrl.StopAttack();
    }

    public void HitAnimFinished()
    {
        hitted = false;
    }

    public int GetNowHealth()
    {
        return nowHealth;
    }
}
