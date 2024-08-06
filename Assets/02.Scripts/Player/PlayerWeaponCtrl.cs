using UnityEngine;

public class PlayerWeaponCtrl : WeaponCtrl
{
    DataCtrl dataCtrl;
    [SerializeField] ParticleSystem startEff;
    [SerializeField] GameObject dustPrefab, slashPrefab;
    [SerializeField] Transform dustTrans, slashTrans;
    [SerializeField] CapsuleCollider auraCol;
    [SerializeField] AudioSource auraSource;
    [SerializeField] AudioSource hitSource;
    public LayerMask layerMask;

    private void Awake()
    {
        dataCtrl = charaterCtrl.GetComponent<PlayerCtrl>().GetDataCtrl();
    }

    protected override void StartAttackCon()
    {
        base.StartAttackCon();
        if (dataCtrl.weaponLv > 100)
        {
            charaterCtrl.dmgImune = true;
        }
        atkPower = (int)(charaterCtrl.atkPower * (1 + (float)dataCtrl.weaponLv / 100));
        float size = Mathf.Clamp(1 - 8 / Mathf.Pow(dataCtrl.weaponLv, 0.5f), 0.01f, 1);
        var main = startEff.main;
        main.startSize = size;
        main.startLifetime = size;
        auraCol.radius = size;
        auraCol.height = size * 8;
        auraCol.center = new Vector3(0, 0, size * 4);
        auraCol.enabled = true;
        auraSource.volume = size / 2;
        auraSource.Play();
        startEff.Play();
    }

    protected override void EndOfAttackPositionCon()
    {
        base.EndOfAttackPositionCon();
        charaterCtrl.dmgImune = false;
        RaycastHit hit;
        if (Physics.Raycast(dustTrans.position, Vector3.down, out hit, 1.5f, layerMask))
        {
            Instantiate(dustPrefab, hit.point + new Vector3(0, 0.1f, 0), Quaternion.Euler(hit.normal)).GetComponent<DustAura>().Set(dataCtrl.weaponLv, atkPower);
        }
        if (dataCtrl.weaponLv >= 1000)
            Instantiate(slashPrefab, slashTrans.position, slashTrans.rotation).GetComponent<SlashAura>().Set(dataCtrl.weaponLv, atkPower);
    }

    protected override void StopAttackCon()
    {
        base.StopAttackCon();
        startEff.Stop();
        auraSource.Stop();
        auraCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaterCtrl targetCharacter;
        if (other.TryGetComponent(out targetCharacter))
        {
            hitSource.Play();
        }
    }
}
