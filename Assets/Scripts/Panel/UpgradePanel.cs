using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    //0 = Max, 1 = MaxOnce, 2 = Speed

    public UpgradeBtn[] upBtns;

    void Start()
    {
        for (int i = 0; i < upBtns.Length; i++)
        {
            DataManager.Instance.LoadUpgradeData(ref upBtns[i], i);
        }

        upBtns[0].btn.onClick.AddListener(UpgradeMax);
        upBtns[1].btn.onClick.AddListener(UpgradeMaxOnce);
        upBtns[2].btn.onClick.AddListener(UpgradeSpeed);
        upBtns[3].btn.onClick.AddListener(UpgradeMaxLife);
        upBtns[4].btn.onClick.AddListener(UpgradeCreatSpeed);

        UpdateTexts(upBtns[0], GameManager.Instance.maxBlood);
        UpdateTexts(upBtns[1], GameManager.Instance.maxBloodOnce);
        UpdateTexts(upBtns[2], GameManager.Instance.suckPerTime);
        UpdateTexts(upBtns[3], GameManager.Instance.maxLife);
        UpdateTexts(upBtns[4], GameManager.Instance.clickAmount);
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            for (int i = 0; i < upBtns.Length; i++)
            {
                DataManager.Instance.SaveUpgradeData(upBtns[i], i);
            }
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < upBtns.Length; i++)
        {
            DataManager.Instance.SaveUpgradeData(upBtns[i], i);
        }
    }

    private void UpgradeMax()
    {
        Upgrade(upBtns[0], ref GameManager.Instance.maxBlood);
    }
    private void UpgradeMaxOnce()
    {
        Upgrade(upBtns[1], ref GameManager.Instance.maxBloodOnce);
    }
    private void UpgradeSpeed()
    {
        Upgrade(upBtns[2], ref GameManager.Instance.suckPerTime);
    }

    private void UpgradeMaxLife()
    {
        Upgrade(upBtns[3], ref GameManager.Instance.maxLife);
    }

    private void UpgradeCreatSpeed()
    {
        Upgrade(upBtns[4], ref GameManager.Instance.clickAmount);
    }

    private void Upgrade(UpgradeBtn upBtn, ref float value)
    {
        if (GameManager.Instance.CheckBlood(upBtn.cost))
        {
            value += upBtn.amount;

            upBtn.level++;
            upBtn.levelText.text = string.Concat("LV ", upBtn.level);

            upBtn.beforeText.text = string.Concat("현재 : ", value);
            upBtn.afterText.text = string.Concat("업그레이드 후 : ", (value + upBtn.amount));

            upBtn.cost += upBtn.addCost;
            upBtn.costText.text = string.Concat("비용 : ", upBtn.cost);

            GameManager.Instance.UseBlood(upBtn.cost);
            UIManager.Instance.UpdateMaxBloodText();
        }
    }

    private void Upgrade(UpgradeBtn upBtn, ref int value)
    {
        if (GameManager.Instance.CheckBlood(upBtn.cost))
        {
            value += (int)upBtn.amount;

            upBtn.level++;
            upBtn.levelText.text = string.Concat("LV ", upBtn.level);

            upBtn.beforeText.text = string.Concat("현재 : ", value);
            upBtn.afterText.text = string.Concat("업그레이드 후 : ", (value + upBtn.amount));

            upBtn.cost += upBtn.addCost;
            upBtn.costText.text = string.Concat("비용 : ", upBtn.cost);

            GameManager.Instance.UseBlood(upBtn.cost);
            UIManager.Instance.UpdateMaxBloodText();
        }
    }

    private void UpdateTexts(UpgradeBtn upBtn, float value)
    {
        upBtn.levelText.text = string.Concat("LV ", upBtn.level);

        upBtn.beforeText.text = string.Concat("현재 : ", value);
        upBtn.afterText.text = string.Concat("업그레이드 후 : ", (value + upBtn.amount));

        upBtn.costText.text = string.Concat("비용 : ", upBtn.cost);
    }

    private void UpdateTexts(UpgradeBtn upBtn, int value)
    {
        upBtn.levelText.text = string.Concat("LV ", upBtn.level);

        upBtn.beforeText.text = string.Concat("현재 : ", value);
        upBtn.afterText.text = string.Concat("업그레이드 후 : ", (value + upBtn.amount));

        upBtn.costText.text = string.Concat("비용 : ", upBtn.cost);
    }
}