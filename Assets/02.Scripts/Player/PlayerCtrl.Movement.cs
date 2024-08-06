using Cinemachine;
using System;
using UnityEngine;

public partial class PlayerCtrl : CharaterCtrl
{
    float j;

    bool rolling;

    protected int MoveHorAnimId = Animator.StringToHash("MoveHor");
    protected int MoveVerAnimId = Animator.StringToHash("MoveVer");
    protected int SlowAnimId = Animator.StringToHash("Slow");

    Vector2 dir => new Vector3(!canMove || onAttack ? 0 : Input.GetAxis("Horizontal"), !canMove || onAttack ? 0 : Input.GetAxis("Vertical"));

    [SerializeField]
    int speedTester = 3;
    int speed => canMove ? (rolling ? 3 * MainSpeed * speedTester : (dir.x != 0 || dir.y != 0 ? MainSpeed * speedTester : 0)) : 0;
    Vector3 xDir => transform.right * dir.x * speed;
    Vector3 yDir => new Vector3(0, j, 0);
    Vector3 zDir => transform.forward * dir.y * speed;
    //Vector3 deg2vec => new Vector3(1, 0, Mathf.Tan(-transform.eulerAngles.y * Mathf.Deg2Rad));

    float rotX => Input.GetAxis("Mouse X");
    float rotY => Input.GetAxis("Mouse Y");

    [SerializeField]
    CinemachineVirtualCamera vcam;
    //CinemachineTransposer vcamTranspo;

    [SerializeField]
    Transform camFollowTrans, slashTrans;

    void ClickMovement()
    {
        slashTrans.rotation = camTrans.rotation;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WindowCtrl();
        }

        animator.SetFloat(SlowAnimId, (float)MainSpeed / 2);

        ground = groundChecker.IsGround();

        horSpeed = dir.x;
        verSpeed = dir.y;

        if (canMove)
        {
            if (Math.Abs(rotX) > 0.2f) transform.Rotate(0, rotX * 20 * Time.deltaTime, 0);
            if (Math.Abs(rotY) > 0.2f) camFollowTrans.localPosition = new Vector3(0, Math.Clamp(camFollowTrans.localPosition.y - rotY * 10 * Time.deltaTime, 0, 5), -7);
            if (dir.x != 0 || dir.y != 0)
            {
                Move();
            }
            else
            {
                Stop();
            }

            if (ground)
            {
                if (!rolling && Input.GetKeyDown(KeyCode.Space))
                {
                    StopAttack();
                    j = 5;
                    AudioSource.Play();
                    Jump();
                    groundChecker.Jump();
                }
                else if (!rolling && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    StopAttack();
                    Roll();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            atkSpeed = Mathf.Clamp(1 + (float)dataCtrl.weaponLv / 1000, 1, 3);

            Attack();
        }

        if (iced && Input.anyKeyDown)
        {
            icedCnt = Mathf.Clamp(icedCnt - 0.05f, 0, float.MaxValue);
        }

        j += Physics.gravity.y * Time.deltaTime;

        if (rolling)
        {
            if (dir.x != 0 || dir.y != 0) characterController.Move((xDir + yDir + zDir) * Time.deltaTime);
            else characterController.Move((transform.forward * speed + yDir) * Time.deltaTime);
        }
        else
            characterController.Move((xDir + yDir + zDir) * Time.deltaTime);
    }

    protected override void Move()
    {
        base.Move();
        animator.SetFloat(MoveHorAnimId, horSpeed);
        animator.SetFloat(MoveVerAnimId, verSpeed);
    }

    void Roll()
    {
        hitted = false;
        dmgImune = true;
        atkCoolCnt = -0.1f;
        animator.SetTrigger(RollAnimId);
    }

    void RollingStart()
    {
        ActiveSource.Stop();
        ActiveSource.PlayOneShot(rollClip);
        rolling = true;
    }

    void RollingEnd()
    {
        dmgImune = false;
        rolling = false;
    }
}
