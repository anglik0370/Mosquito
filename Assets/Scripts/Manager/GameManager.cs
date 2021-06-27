using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";

                    obj.hideFlags = HideFlags.HideAndDontSave; //�޸� ���� ��󿡼� ����

                    instance = obj.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    [Header("������")]
    [SerializeField]
    public int life = 0;
    public int maxLife = 20;

    [Header("������ �� ���� ��")]
    public float suckPerTime = 1f; //������ �� ���� ��

    [Header("���� ��")]
    public float curBlood = 0f;
    public float maxBlood = 1000f;

    [Header("�ѹ��� ���� ���� ��")]
    public float curBloodOnce = 0f;
    public float maxBloodOnce = 100;

    [Header("�� ���� �׾�����")]
    public bool isGameOver = false;

    [Header("������ ���� �ӵ�")]
    public int clickAmount = 1;

    [Header("����׿� ����")]
    public bool bSave = false;

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
    }
    private void Start()
    {
        UIManager.Instance.UpdateLifeText();
        UIManager.Instance.UpdateCurBloodText();
        UIManager.Instance.UpdateMaxBloodText();
        UIManager.Instance.UpdatePackImg();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            DataManager.Instance.SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData();
    }

    public bool CheckLife()
    {
        if ((life - 1) < 0)
        {
            ErrorManager.Instance.SendError("�������� �����մϴ�");
            return false;
        }

        return true;
    }

    public void AddLife(int value)
    {
        if (life + value >= maxLife)
        {
            life = maxLife;
        }
        else
        {
            life += value;
        }

        UIManager.Instance.UpdateLifeText();
    }

    public void UseLife(int value)
    {
        if(life - value < 0)
        {
            ErrorManager.Instance.SendError("�������� �����մϴ�");
            return;
        }

        life -= value;
        UIManager.Instance.UpdateLifeText();
    }

    public bool CheckBlood(int value)
    {
        if (value > curBlood)
        {
            ErrorManager.Instance.SendError("�ǰ� �����մϴ�");
            return false; ;
        }

        return true;
    }

    public void UseBlood(int value)
    {
        if(value > curBlood)
        {
            ErrorManager.Instance.SendError("�ǰ� �����մϴ�");
            return;
        }

        curBlood -= value;
        UIManager.Instance.UpdateCurBloodText();
    }
}
