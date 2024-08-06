using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataCtrl
{
    public int lv;
    public int exp;
    public int weaponLv;
    public uint kill;

    public bool GroundDragon;
    public bool IceDragon;
    public bool VolcanoDragon;
    public bool DeathDragon;
}

public class GameManager : MonoBehaviour
{
    DataCtrl dataCtrl;

    private string filePath;

    public static GameManager Instance;

    public PlayerCtrl PlayerCtrl;

    [SerializeField]
    Transform[] spawnCenterTrans, monsterGroups;

    public byte nowAreaNum;
    public bool onChanging;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            filePath = Path.Combine(Application.dataPath, "PlayerData.json");

            if (File.Exists(filePath))
            {
                Debug.Log("Found file and open file");
                string jsonString = File.ReadAllText(filePath);
                dataCtrl = JsonUtility.FromJson<DataCtrl>(jsonString);
            }
            else
            {
                Debug.Log("No file and create file");

                dataCtrl = new DataCtrl();

                dataCtrl.lv = 1;
                dataCtrl.exp = 0;
                dataCtrl.weaponLv = 1;
                dataCtrl.kill = 0;

                dataCtrl.GroundDragon = false;
                dataCtrl.IceDragon = false;
                dataCtrl.VolcanoDragon = false;
                dataCtrl.DeathDragon = false;

                SaveData();
            }

            PlayerCtrl.SetDataCtrl(dataCtrl);

            PlayerCtrl.gameObject.SetActive(true);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeArea(byte areaNum)
    {
        onChanging = true;
        nowAreaNum = areaNum;
        for (int i = 0; i < spawnCenterTrans.Length; i++)
        {
            if (i == areaNum)
            {
                spawnCenterTrans[i].gameObject.SetActive(true);
            }
            else
            {
                for (int j = 0; j < monsterGroups[i].childCount; j++)
                {
                    Destroy(monsterGroups[i].GetChild(j).gameObject);
                }
                foreach (SpawnCenter sc in spawnCenterTrans[i].GetComponentsInChildren<SpawnCenter>())
                {
                    sc.MonsterKilled();
                }
                foreach (BossSpawnCenter sc in spawnCenterTrans[i].GetComponentsInChildren<BossSpawnCenter>())
                {
                    sc.BossKilled();
                }
                spawnCenterTrans[i].gameObject.SetActive(false);
            }
        }
        onChanging = false;
    }

    public void MonsterKilled(int exp, byte monsterNum)
    {
        PlayerCtrl.MonsterKilled(exp);
    }

    public void BossKilled(byte bossNum)
    {
        switch (bossNum)
        {
            case 0:
                if (!dataCtrl.GroundDragon)
                {

                }
                dataCtrl.GroundDragon = true;
                break;

            case 1:
                if (!dataCtrl.IceDragon)
                {

                }
                dataCtrl.IceDragon = true;
                break;

            case 2:
                if (!dataCtrl.VolcanoDragon)
                {

                }
                dataCtrl.VolcanoDragon = true;
                break;

            case 3:
                if (!dataCtrl.DeathDragon)
                {

                }
                dataCtrl.DeathDragon = true;
                break;
        }

        PlayerCtrl.StatusUpdate();

        SaveData();
    }

    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(dataCtrl);
        File.WriteAllText(filePath, jsonString);
    }
}
