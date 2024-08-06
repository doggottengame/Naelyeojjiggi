using System.Collections;
using UnityEngine;

public class MonsterCtrl : CharaterCtrl
{
    [SerializeField]
    protected float range;
    [SerializeField]
    byte spawnType;

    protected Rigidbody rb;
    protected Transform SpawnCenterPos;
    protected Transform target = null;
    protected Vector3 vel;

    public int exp;
    protected float cnt = 0;
    protected int farRange = 100;

    public bool IsBoss;
    protected bool onSet;
    protected bool onDelay, back2center;

    public virtual void Set(Transform spawnCenter, byte spawnType)
    {
        this.spawnType = spawnType;
        animator.enabled = true;
        foreach (Collider col in transform.GetComponentsInChildren<Collider>())
        {
            if (!col.isTrigger)
                col.enabled = true;
        }
        Collider bodyCol;
        if (TryGetComponent(out bodyCol))
        {
            bodyCol.enabled = true;
        }
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        SpawnCenterPos = spawnCenter;
        SpawnCenter center;
        BossSpawnCenter bossCenter;
        if (spawnCenter.TryGetComponent(out center))
        {
            this.spawnCenter = center;
        }
        else if (spawnCenter.TryGetComponent(out bossCenter))
        {
            bossSpawnCenter = bossCenter;
        }

        onSet = true;
    }

    public void Update()
    {
        if (!onSet || dead) return;
        UpdateCon();
        UpdateCon2();
        MovementCtrl();
    }

    protected virtual void MovementCtrl()
    {
        if (!canMove) return;
        if (target != null)
        {
            Vector3 rot = target.position - transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));

            if ((target.position - transform.position).sqrMagnitude < range)
            {
                vel = Vector3.zero;
                Attack();
            }
            else
            {
                vel = (target.position - transform.position).normalized * 2;
            }
        }
        else
        {
            if (!onDelay)
            {
                if ((SpawnCenterPos.position - transform.position).sqrMagnitude >= farRange)
                {
                    if (!back2center)
                    {
                        onDelay = true;
                        back2center = true;
                        vel = Vector3.zero;
                        Stop();
                        StartCoroutine(MoveDelay(true));
                    }
                }
                if ((cnt -= Time.deltaTime) <= 0)
                {
                    onDelay = true;
                    back2center = false;
                    vel = Vector3.zero;
                    Stop();
                    StartCoroutine(MoveDelay(false));
                }
            }
        }
        vel.y = rb.velocity.y;
        rb.velocity = vel;
        if (vel.x != 0 || vel.z != 0)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    protected virtual void UpdateCon2()
    {

    }

    IEnumerator MoveDelay(bool back2centerFlag)
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        if (back2centerFlag)
        {
            Vector3 rot = SpawnCenterPos.position - transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));
        }
        else
        {
            transform.Rotate(0, Random.Range(-180, 180), 0);
        }

        vel = transform.forward * 2;
        cnt = Random.Range(2f, 4f);
        onDelay = false;
    }

    public virtual void FoundPlayer(Transform target)
    {
        this.target = target;
    }

    public virtual void MissPlayer()
    {
        target = null;
    }

    public virtual void TargetInAura()
    {

    }

    protected override void Death()
    {
        base.Death();
        if (spawnCenter != null) spawnCenter.MonsterKilled();
        else bossSpawnCenter.BossKilled();
        GameManager.Instance.MonsterKilled(exp, spawnType);

        Destroy(gameObject, 3);
    }
}
