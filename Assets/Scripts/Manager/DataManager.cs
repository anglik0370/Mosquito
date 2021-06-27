using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (!instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "DataManager";

                    obj.hideFlags = HideFlags.HideAndDontSave; //메모리 해제 대상에서 제외

                    instance = obj.AddComponent<DataManager>();
                }
            }

            return instance;
        }
    }

    string path;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        path = Application.persistentDataPath + "/" + "MosquitoStat.txt";
    }

    public void SaveData()
    {
        //DataVO data = new DataVO();

        //data.life = GameManager.Instance.life;
        //data.maxLife = GameManager.Instance.maxLife;

        //data.suckPerTime = GameManager.Instance.suckPerTime;

        //data.curBlood = GameManager.Instance.curBlood;
        //data.maxBlood = GameManager.Instance.maxBlood;

        //data.curBloodOnce = GameManager.Instance.curBloodOnce;
        //data.maxBloodOnce = GameManager.Instance.maxBloodOnce;

        //data.clickAmount = GameManager.Instance.clickAmount;

        //string json = JsonUtility.ToJson(data, true);

        //File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        //if (!File.Exists(path))
        //{
        //    SaveData();
        //}

        //string json = File.ReadAllText(path);

        //DataVO data = JsonUtility.FromJson<DataVO>(json);

        //GameManager.Instance.life = data.life;
        //GameManager.Instance.maxLife = data.maxLife;

        //GameManager.Instance.suckPerTime = data.suckPerTime;

        //GameManager.Instance.curBlood = data.curBlood;
        //GameManager.Instance.maxBlood = data.maxBlood;

        //GameManager.Instance.curBloodOnce = data.curBloodOnce;
        //GameManager.Instance.maxBloodOnce = data.maxBloodOnce;

        //GameManager.Instance.clickAmount = data.clickAmount;
    }

    public void SaveUpgradeData(UpgradeBtn upbtn, int value)
    {
        //string path = Application.persistentDataPath + "/" + "UpgradeStat" + value +".txt";

        //UpgradeVO upVO = new UpgradeVO();

        //upVO.level = upbtn.level;
        //upVO.addCost = upbtn.addCost;
        //upVO.amount = upbtn.amount;
        //upVO.cost = upbtn.cost;

        //string json = JsonUtility.ToJson(upVO, true);

        //File.WriteAllText(path, json);
    }

    public void LoadUpgradeData(ref UpgradeBtn upbtn, int value)
    {
        //string path = Application.persistentDataPath + "/" + "UpgradeStat" + value + ".txt";

        //if (!File.Exists(path))
        //{
        //    SaveUpgradeData(upbtn, value);
        //}

        //string json = File.ReadAllText(path);

        //UpgradeVO upVO = JsonUtility.FromJson<UpgradeVO>(json);

        //upbtn.level = upVO.level;
        //upbtn.addCost = upVO.addCost;
        //upbtn.amount = upVO.amount;
        //upbtn.cost = upVO.cost;
    }
}
