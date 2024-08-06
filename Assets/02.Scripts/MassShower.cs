using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TextType
{
    Damage,
    Heal,
}

public class MassShower : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        Vector3 rot = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 180 + Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg, 0));

        Destroy(gameObject, 3);
    }

    public virtual void Set(int mass, TextType textType)
    {
        text.text = $"{mass}";
        GetComponent<Rigidbody>().AddRelativeForce(new Vector2((Random.Range(0, 2) - 0.5f) * 2, 1) * Random.Range(40, 50));
        switch (textType)
        {
            case TextType.Damage:
                text.color = Color.black;
                break;

            case TextType.Heal:
                text.color = Color.green;
                break;
        }
    }
}
