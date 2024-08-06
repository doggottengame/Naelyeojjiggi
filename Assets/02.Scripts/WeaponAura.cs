using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAura : MonoBehaviour
{
    [SerializeField]
    PlayerCtrl charaterCtrl;
    DataCtrl dataCtrl;

    int atkPower => (int) (charaterCtrl.atkPower* (1 + (float) dataCtrl.weaponLv / 100));

    private void Awake()
    {
        dataCtrl = charaterCtrl.GetDataCtrl();
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
