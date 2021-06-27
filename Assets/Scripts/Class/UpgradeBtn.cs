using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeBtn
{
    public Button btn;
    public Text beforeText;
    public Text afterText;
    public Text levelText;
    public Text costText;
    public int cost;
    public int addCost;
    public float amount;
    public int level;
}
