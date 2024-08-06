using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCtrl : MonoBehaviour
{
    protected CharaterCtrl charaterCtrl;
    protected Transform target;

    public void Set(CharaterCtrl charaterCtrl, Transform target)
    {
        this.charaterCtrl = charaterCtrl;
        this.target = target;
    }

    public CharaterCtrl GetSelfCtrl()
    {
        return charaterCtrl;
    }

    public Transform GetTargetCtrl()
    {
        return target;
    }
}
