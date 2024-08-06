using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCtrl : MonsterCtrl
{
    protected WeaponCtrl curWeapon = null;
    [SerializeField]
    protected float[] otherRange;

    protected Action[] AttackByRange;

    protected int SleepAnimId = Animator.StringToHash("Sleep");
    protected int FlyAnimId = Animator.StringToHash("Fly");
    protected int LandAnimId = Animator.StringToHash("Land");
    protected int AttackNumAnimId = Animator.StringToHash("AttackNum");
    protected int OnMagicAnimId = Animator.StringToHash("OnMagic");

    [SerializeField]
    protected GameObject magicPrefab;

    protected Vector3 rotVel;

    [SerializeField]
    BoxCollider landCol;

    [SerializeField]
    AudioClip screamClip, screamLongClip;

    protected void Set()
    {
        try
        {
            animator.GetBehaviour<StartDragonMagic>().Set(this);
            animator.GetBehaviour<EndDragonMagic>().Set(this);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        IsBoss = true;
        AttackByRange = new Action[otherRange.Length + 1];
    }

    protected override void MovementCtrl()
    {
        if (!canMove || atkCoolCnt > 0) return;
        if (target != null)
        {
            if ((transform.position - SpawnCenterPos.position).sqrMagnitude >= 400)
            {
                target = null;
                Vector3 rot = SpawnCenterPos.position - transform.position;
                Quaternion rotQua = Quaternion.Euler(new Vector3(0, Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));
                transform.rotation = Quaternion.Lerp(transform.rotation, rotQua, Time.deltaTime * 4);

                if (Quaternion.Angle(rotQua, transform.rotation) > 10)
                {
                    vel = Vector3.zero;
                }
                else
                {
                    vel = transform.forward * 2;
                }
                Move();
            }
            else
            {
                if (!onAttack)
                {
                    Vector3 rot = target.position - transform.position;
                    Quaternion rotQua = Quaternion.Euler(new Vector3(0, Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotQua, Time.deltaTime * 4);

                    Move();
                    if (Quaternion.Angle(rotQua, transform.rotation) > 10)
                    {
                        vel = Vector3.zero;
                    }
                    else
                    {
                        vel = transform.forward * 2;

                        if ((cnt -= Time.deltaTime) > 0) return;
                        if ((target.position - transform.position).sqrMagnitude < range)
                        {
                            atkCoolCnt = atkCool;
                            onAttack = true;
                            vel = Vector3.zero;
                            Stop();
                            AttackByRange[0]();
                        }
                        else
                        {
                            for (int i = 0; i < otherRange.Length; i++)
                            {
                                if ((target.position - transform.position).sqrMagnitude < otherRange[i])
                                {
                                    atkCoolCnt = atkCool;
                                    onAttack = true;
                                    vel = Vector3.zero;
                                    Stop();
                                    AttackByRange[i + 1]();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if ((transform.position - SpawnCenterPos.position).sqrMagnitude >= 1)
            {
                Vector3 rot = SpawnCenterPos.position - transform.position;
                Quaternion rotQua = Quaternion.Euler(new Vector3(0, Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));
                transform.rotation = Quaternion.Lerp(transform.rotation, rotQua, Time.deltaTime * 4);

                if (Quaternion.Angle(rotQua, transform.rotation) > 10)
                {
                    vel = Vector3.zero;
                }
                else
                {
                    vel = transform.forward * 2;
                }

                Move();
                landCol.center = new Vector3(0, 1.4f, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }
            else
            {
                transform.rotation = Quaternion.identity;

                vel = Vector3.zero;

                Stop();
                animator.SetBool(SleepAnimId, true);
                landCol.center = new Vector3(0, 1.2f, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    protected override void Move()
    {
        base.Move();
        animator.SetBool(SleepAnimId, false);
    }
    public override void StartAttack()
    {
        if (curWeapon != null)
        {
            ActiveSource.Stop();
            ActiveSource.clip = swingClip;
            ActiveSource.Play();
            curWeapon.StartAttack();
        }
    }

    public override void StopAttack()
    {
        if (curWeapon != null)
        {
            curWeapon.StopAttack();
        }

        cnt = 1;
        onAttack = false;
        animator.SetInteger(AttackNumAnimId, 0);
    }

    void Scream()
    {
        ActiveSource.clip = screamClip;
        ActiveSource.Play();
    }

    void ScreamLong()
    {
        ActiveSource.clip = screamLongClip;
        ActiveSource.Play();
    }

    public virtual void MagicStart()
    {
        
    }

    public virtual void MagicEnd()
    {
        onAttack = false;
    }

    public virtual void BreathHitToPlayer()
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHit(atkPower / 50, DmgType.Normal, 0);
        }
    }

    public virtual void MagicHitToPlayer(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.Hit(atkPower / dmg, DmgType.Normal, 0);
        }
    }

    public virtual void TargetInFear(int dmg)
    {
        CharaterCtrl charaterCtrl;
        if (target.TryGetComponent(out charaterCtrl))
        {
            charaterCtrl.FixedHitNoAction(atkPower / dmg, DmgType.Normal, 0);
        }
    }

    public override void FoundPlayer(Transform target)
    {
        this.target = target;
        animator.SetBool(SleepAnimId, false);
    }
}
