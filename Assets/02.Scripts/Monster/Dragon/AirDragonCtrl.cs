using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDragonCtrl : DragonCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;
    [SerializeField]
    WeaponCtrl weaponCtrl_claw;

    [SerializeField]
    ParticleSystem breathParticle;

    protected bool onBreath = false;
    protected bool onFly = false;

    [SerializeField]
    protected AudioSource AudioSource;
    [SerializeField]
    AudioClip biteClip, clawClip, breathClip;

    public void Awake()
    {
        ccImune = true;
        Set(50000 + 20000 * (int)(worldNum * 1.5f), 3000 + 2000 * (int)(worldNum * 1.5f), 500 + 1500 * (int)(worldNum * 1.5f), 40 + 10 * worldNum, 5);
        exp += 100000 * worldNum;
        atkSpeed = 1;
        Set();

        AttackByRange[0] = AttackBite;
        AttackByRange[1] = AttackClaw;
        AttackByRange[2] = AttackBreath;
        AttackByRange[3] = AttackFlyBreath;
        AttackByRange[4] = AttackMagic;

        foreach (DragonTakeOff dL in animator.GetBehaviours<DragonTakeOff>())
        {
            dL.Set(this);
        }
        foreach (DragonLanding dL in animator.GetBehaviours<DragonLanding>())
        {
            dL.Set(this);
        }
    }

    protected override void Move()
    {

    }

    protected override void MovementCtrl()
    {
        base.MovementCtrl();

        vel = Vector3.zero;
        
        if (!onFly)
        {
            vel.y = rb.velocity.y;
        }
        rb.velocity = vel;
    }

    public void TakeOff()
    {
        onFly = true;
        rb.useGravity = false;
        animator.SetBool(LandAnimId, false);
        atkSpeed = 0.5f;
        animator.SetFloat(AtkSpeedAnimId, atkSpeed);
    }

    public void Landing()
    {
        onFly = false;
        rb.useGravity = true;
        animator.SetBool(LandAnimId, true);
        atkSpeed = 0.5f;
        animator.SetFloat(AtkSpeedAnimId, atkSpeed);
    }

    void SwingWing()
    {
        AudioSource.Play();
    }

    void BreathStart()
    {
        ActiveSource.clip = swingClip;
        ActiveSource.Play();
        breathParticle.trigger.AddCollider(target.GetComponent<Collider>());
        breathParticle.Play();
        atkSpeed = 0.25f;
        animator.SetFloat(AtkSpeedAnimId, atkSpeed);
    }

    void BreathStop()
    {
        ActiveSource.Stop();
        onBreath = false;
        onAttack = false;
        breathParticle.Stop();
        if (target != null)
            breathParticle.trigger.RemoveCollider(target.GetComponent<Collider>());
        atkSpeed = 0.5f;
        animator.SetFloat(AtkSpeedAnimId, atkSpeed);
        animator.SetBool(LandAnimId, true);
    }

    void AttackMagic()
    {
        curWeapon = null;
        atkSpeed = 1;
        animator.SetInteger(AttackNumAnimId, 3);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackFlyBreath()
    {
        swingClip = breathClip;
        onBreath = true;
        curWeapon = null;
        atkSpeed = 0.5f;
        animator.SetTrigger(FlyAnimId);
        animator.SetInteger(AttackNumAnimId, 4);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackBreath()
    {
        swingClip = breathClip;
        onBreath = true;
        curWeapon = null;
        atkSpeed = 0.5f;
        animator.SetInteger(AttackNumAnimId, 2);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackClaw()
    {
        swingClip = clawClip;
        curWeapon = weaponCtrl_claw;
        atkSpeed = 1;
        animator.SetInteger(AttackNumAnimId, 1);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackBite()
    {
        swingClip = biteClip;
        curWeapon = weaponCtrl;
        atkSpeed = 1;
        animator.SetInteger(AttackNumAnimId, 0);
        animator.SetTrigger(AttackAnimId);
    }

    protected override void Death()
    {
        base.Death();
        BreathStop();
        AudioSource.Stop();
    }
}
