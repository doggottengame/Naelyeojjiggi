using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WolfGroup : MonoBehaviour
{
    [SerializeField]
    bool elite;

    byte spawnType;

    [SerializeField]
    WolfCtrl wolfCtrl;
    Transform wolfTrans;
    [SerializeField]
    GameObject childPrefab;

    float cnt = 0;

    public void Set(Transform spawnCenter, byte spawnType)
    {
        this.spawnType = spawnType;

        wolfTrans = wolfCtrl.transform;

        transform.GetChild(0).GetComponent<WolfCtrl>().enabled = true;
        transform.GetChild(0).GetComponent<WolfCtrl>().Set(spawnCenter, spawnType);

        transform.GetChild(1).GetComponent<WolfChildCtrl>().enabled = true;
        transform.GetChild(1).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
        transform.GetChild(1).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);

        transform.GetChild(2).GetComponent<WolfChildCtrl>().enabled = true;
        transform.GetChild(2).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
        transform.GetChild(2).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);

        transform.GetChild(3).GetComponent<WolfChildCtrl>().enabled = true;
        transform.GetChild(3).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
        transform.GetChild(3).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);

        if (elite)
        {
            transform.GetChild(4).GetComponent<WolfChildCtrl>().enabled = true;
            transform.GetChild(4).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
            transform.GetChild(4).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);

            transform.GetChild(5).GetComponent<WolfChildCtrl>().enabled = true;
            transform.GetChild(5).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
            transform.GetChild(5).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);

            transform.GetChild(6).GetComponent<WolfChildCtrl>().enabled = true;
            transform.GetChild(6).GetComponent<WolfChildCtrl>().Set(wolfTrans, spawnType);
            transform.GetChild(6).GetComponent<WolfChildCtrl>().SetBoss(wolfCtrl);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wolfCtrl == null || transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        else if (transform.childCount < 4)
        {
            if ((cnt += Time.deltaTime) >= 5)
            {
                WolfChildCtrl wolfChildCtrl = Instantiate(childPrefab, wolfTrans.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), wolfTrans.rotation).GetComponent<WolfChildCtrl>();
                wolfChildCtrl.enabled = true;
                wolfChildCtrl.Set(wolfTrans, spawnType);
                wolfChildCtrl.SetBoss(wolfCtrl);
                cnt = 0;
            }
        }
    }
}
