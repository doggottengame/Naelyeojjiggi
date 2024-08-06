using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashAura : MonoBehaviour
{
    [SerializeField] Transform particleTrans;
    float duration, cnt;
    Vector3 slashScale, particleScale;

    int atkPower = 0;
    int finAtkPower = 0;

    public void Set(int lv, int atkPower)
    {
        this.atkPower = (int)(atkPower * (1 + (float)lv / 100));
        finAtkPower = atkPower;

        transform.localScale = new Vector3(1, Mathf.Clamp(1 - 20 / Mathf.Pow(lv, 0.5f), 1, 2), 1);
        particleTrans.localScale = new Vector3(0.2f, Mathf.Clamp(1 - 20 / Mathf.Pow(lv, 0.5f), 1, 2) / 5, 0.2f);
        slashScale = transform.localScale;
        particleScale = particleTrans.localScale;

        duration = Mathf.Clamp(1 - 15 / Mathf.Pow(lv, 0.5f), 1, 2);
        cnt = duration * 1.5f;

        GetComponent<Rigidbody>().velocity = transform.forward * 10;
    }

    private void Update()
    {
        cnt -= Time.deltaTime;
        transform.localScale = slashScale * Mathf.Clamp(cnt / duration, 0, 1);
        particleTrans.localScale = particleScale * Mathf.Clamp(cnt / duration, 0, 1);
        finAtkPower = atkPower * (int)Mathf.Clamp(cnt / duration, 0, 1);

        if (cnt <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaterCtrl targetCharacter;
        if (other.TryGetComponent(out targetCharacter))
        {
            targetCharacter.Hit(finAtkPower, DmgType.Normal, 0);
        }
        else
        {
            for (Transform targetRoot = other.gameObject.transform.parent; targetRoot != null && !targetRoot.TryGetComponent(out targetCharacter); targetRoot = targetRoot.parent) ;

            if (targetCharacter != null) targetCharacter.Hit(finAtkPower, DmgType.Normal, 0);
        }
    }
}
