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

                    obj.hideFlags = HideFlags.HideAndDontSave; //메모리 해제 대상에서 제외

                    instance = obj.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    [Header("라이프")]
    [SerializeField]
    public int life = 0;
    public int maxLife = 20;

    [Header("딜레이 당 빠는 양")]
    public float suckPerTime = 1f; //딜레이 당 빠는 양

    [Header("피의 양")]
    public float curBlood = 0f;
    public float maxBlood = 1000f;

    [Header("한번에 빠는 피의 양")]
    public float curBloodOnce = 0f;
    public float maxBloodOnce = 100;

    [Header("모기가 팔에 앉았는지")]
    public bool isDown = false;

    [Header("DNA 보유에 따른 배율")]
    public int beaSu = 1;

    [Header("피 빨다 죽었는지")]
    public bool isGameOver = false;

    [Header("라이프 제작 속도")]
    public int clickAmount = 1;

    [Header("디버그용 변수")]
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
        DataManager.Instance.LoadData();

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

    public bool CheckLife(int value)
    {
        if (life - value < 0)
        {
            ErrorManager.Instance.SendError("라이프가 부족합니다");
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
        life -= value;
        UIManager.Instance.UpdateLifeText();
    }

    public bool CheckBlood(int value)
    {
        if (value > curBlood)
        {
            ErrorManager.Instance.SendError("피가 부족합니다");
            return false;
        }

        return true;
    }

    public void UseBlood(int value)
    {
        curBlood -= value;
        UIManager.Instance.UpdateCurBloodText();
    }
}
