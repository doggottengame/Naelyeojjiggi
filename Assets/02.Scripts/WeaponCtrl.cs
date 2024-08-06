using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    [SerializeField] protected CharaterCtrl charaterCtrl;
    [SerializeField] Collider weaponCol;

    protected int atkPower = 0;

    public void StartAttack()
    {
        weaponCol.enabled = true;
        atkPower = charaterCtrl.atkPower;
        StartAttackCon();
    }

    protected virtual void StartAttackCon()
    {

    }

    public void EndOfAttackPosition()
    {
        EndOfAttackPositionCon();
    }

    protected virtual void EndOfAttackPositionCon()
    {

    }

    public void StopAttack()
    {
        weaponCol.enabled = false;
        atkPower = 0;
        StopAttackCon();
    }

    protected virtual void StopAttackCon()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CharaterCtrl targetCharacter;
        if (other.TryGetComponent(out targetCharacter))
        {
            targetCharacter.Hit(atkPower, DmgType.Normal, 0);
        }
    }
}
