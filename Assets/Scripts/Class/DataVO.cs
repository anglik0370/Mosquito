using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataVO
{
    public int life = 0;
    public int maxLife = 20;

    public float suckPerTime = 1f;

    public float curBlood = 0f;
    public float maxBlood = 1000f;

    public float curBloodOnce = 0f;
    public float maxBloodOnce = 100;

    public int clickAmount = 1;

    public int baesu = 1;
}
