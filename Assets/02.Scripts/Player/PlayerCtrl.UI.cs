using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerCtrl : CharaterCtrl
{
    [SerializeField]
    GameObject Window;
    [SerializeField]
    GameObject slowIcon, iceIcon, burnIcon, deathIcon, bossHpBar;
    [SerializeField]
    Image slowImage, iceImage, burnImage, deathImage, bossHpBarImage;
    [SerializeField]
    Image hpBarImg, expBarImg;
    [SerializeField]
    TMP_Text slowCntTxt, iceCntTxt, burnStackTxt, deathStackTxt, bossHpBarTxt;
    [SerializeField]
    TMP_Text hpTxt, lvTxt, killTxt;

    void WindowCtrl()
    {
        Window.SetActive(!Window.activeSelf);
    }

    void UIUpdate()
    {
        slowIcon.SetActive(slowCnt > 0);
        iceIcon.SetActive(icedCnt > 0);
        burnIcon.SetActive(BurnStack > 0);
        deathIcon.SetActive(DeathStack > 0);

        slowImage.fillAmount = 1 - slowCnt / lastSlowCnt;
        iceImage.fillAmount = 1 - icedCnt / 3;
        burnImage.fillAmount = 1 - burnCnt;
        deathImage.fillAmount = 1 - deathCnt;

        slowCntTxt.text = slowCnt.ToString("#.##");
        iceCntTxt.text = icedCnt.ToString("#.##");
        burnStackTxt.text = BurnStack.ToString();
        deathStackTxt.text = DeathStack.ToString();

        BossHpUICtrl();
    }

    void HpBarCtrl()
    {
        hpBarImg.fillAmount = (float)nowHealth / health;
        hpTxt.text = $"{nowHealth} / {health}";
    }

    void ExpBarCtrl()
    {
        expBarImg.fillAmount = dataCtrl.exp / Mathf.Pow(dataCtrl.lv, 4f / 3f);
        lvTxt.text = $"Lv.{dataCtrl.lv}";
    }

    void KillRecordCtrl()
    {
        killTxt.text = $"{dataCtrl.kill}";
    }

    MonsterCtrl targetBossCharacterCtrl;

    void BossHpUICtrl()
    {
        if (targetBossCharacterCtrl == null)
        {
            bossHpBar.SetActive(false);
            return;
        }
        bossHpBar.SetActive(true);
        bossHpBarImage.fillAmount = (float)targetBossCharacterCtrl.GetNowHealth() / targetBossCharacterCtrl.health;
        bossHpBarTxt.text = $"{targetBossCharacterCtrl.GetNowHealth()} / {targetBossCharacterCtrl.health}";
    }
}
