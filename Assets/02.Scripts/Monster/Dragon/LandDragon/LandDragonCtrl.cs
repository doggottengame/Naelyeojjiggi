using System;
using System.Collections;
using UnityEngine;

public class LandDragonCtrl : DragonCtrl
{
    [SerializeField]
    [Range(1, 4)] byte worldNum;
    protected int JumpCountAnimId = Animator.StringToHash("JumpCount");

    [SerializeField]
    WeaponCtrl weaponCtrl_claw;
    [SerializeField]
    WeaponCtrl weaponCtrl_horn;
    [SerializeField]
    WeaponCtrl weaponCtrl_jump;
    [SerializeField]
    LayerMask layerMask;

    int jumpAttackCount;

    protected bool onDash, onJump;

    [SerializeField]
    AudioClip[] attackCloseClip;
    [SerializeField]
    AudioClip attackTooCloseClip, dashClip;

    public void Awake()
    {
        Set(60000 + 30000 * (int)(worldNum * 1.5f), 1800 + 800 * (int)(worldNum * 1.5f), 2500 + 900 * (int)(worldNum * 1.5f), 50 + 10 * worldNum, 5);
        exp += 100000 * worldNum;
        atkSpeed = 1;
        Set();

        AttackByRange[0] = AttackTooCloseOrLargeAngle;
        AttackByRange[1] = AttackClose;
        AttackByRange[2] = AttackMagic;
        AttackByRange[3] = AttackHorn;
    }

    protected override void MovementCtrl()
    {
        base.MovementCtrl();

        if (onDash)
        {
            vel = transform.forward * 10;
        }
        else
        {
            vel = Vector3.zero;
        }
        vel.y = rb.velocity.y;
        rb.velocity = vel;
    }

    public override void StopAttack()
    {
        if (onJump)
        {
            animator.SetInteger(JumpCountAnimId, jumpAttackCount++);
            weaponCtrl_jump.StopAttack();
            if (jumpAttackCount > 2)
            {
                onJump = false;
                onAttack = false;
                animator.SetInteger(AttackNumAnimId, 0);

                cnt = 1;
            }
        }
        else
        {
            base.StopAttack();
        }
    }

    void AttackHorn()
    {
        curWeapon = weaponCtrl_horn;
        atkSpeed = 1;
        animator.SetInteger(AttackNumAnimId, 4);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackMagic()
    {
        curWeapon = null;
        atkSpeed = 0.5f;
        animator.SetInteger(AttackNumAnimId, 3);
        animator.SetTrigger(AttackAnimId);
    }

    void AttackClose()
    {
        Action[] attackType = new Action[]
        {
            () => {
                curWeapon = weaponCtrl;
                atkSpeed = 1;
                animator.SetInteger(AttackNumAnimId, 1);
                animator.SetTrigger(AttackAnimId);
            },
            () => {
                curWeapon = weaponCtrl_claw;
                atkSpeed = 1;
                animator.SetInteger(AttackNumAnimId, 2);
                animator.SetTrigger(AttackAnimId);
            }
        };

        int t = UnityEngine.Random.Range(0, attackType.Length);
        attackType[t]();
        swingClip = attackCloseClip[t];
    }

    void AttackTooCloseOrLargeAngle()
    {
        swingClip = attackTooCloseClip;
        onJump = true;
        jumpAttackCount = 0;
        curWeapon = weaponCtrl_jump;
        atkSpeed = 1;
        animator.SetInteger(AttackNumAnimId, 0);
        animator.SetTrigger(AttackAnimId);
    }

    public void DashStart()
    {
        ActiveSource.Stop();
        swingClip = dashClip;
        ActiveSource.PlayOneShot(swingClip);
        swingClip = null;
        onDash = true;
    }

    public void DashStop()
    {
        onDash = false;
    }
}
