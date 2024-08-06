using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
    [SerializeField]
    byte changeAreaNum;

    bool onChanging;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.onChanging || onChanging) return;
        onChanging = true;
        if (GameManager.Instance.nowAreaNum == 0)
        {
            if (changeAreaNum == 0)
            {
                GameManager.Instance.ChangeArea(3);
            }
            else if (changeAreaNum == 1)
            {
                GameManager.Instance.ChangeArea(1);
            }
        }
        else if (GameManager.Instance.nowAreaNum == 1)
        {
            if (changeAreaNum == 0)
            {
                GameManager.Instance.ChangeArea(2);
            }
            else if (changeAreaNum == 1)
            {
                GameManager.Instance.ChangeArea(0);
            }
        }
        else if (GameManager.Instance.nowAreaNum == 2)
        {
            if (changeAreaNum == 0)
            {
                GameManager.Instance.ChangeArea(1);
            }
            else if (changeAreaNum == 1)
            {
                GameManager.Instance.ChangeArea(3);
            }
        }
        else if (GameManager.Instance.nowAreaNum == 3)
        {
            if (changeAreaNum == 0)
            {
                GameManager.Instance.ChangeArea(0);
            }
            else if (changeAreaNum == 1)
            {
                GameManager.Instance.ChangeArea(2);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onChanging = false;
    }
}