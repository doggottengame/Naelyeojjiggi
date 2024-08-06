using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAura : MonoBehaviour
{
    int atkPower;
    [SerializeField]
    AudioSource dustSource;

    public void Set(int lv, int atkPower)
    {
        this.atkPower = (int)(atkPower * (1 + (float)lv / 100));
        float f = Mathf.Clamp((1 - Mathf.Pow(lv, 0.5f)) / 10, 0.001f, 1);
        Vector3 scale = new Vector3(f, f, f);
        dustSource.volume = f;
        foreach (Transform t in transform.GetComponentInChildren<Transform>())
        {
            t.localScale = scale;
        }

        Destroy(gameObject, 1.5f);
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
