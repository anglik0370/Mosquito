using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(UIManager)) as UIManager;
                if (!instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "UIManager";

                    obj.hideFlags = HideFlags.HideAndDontSave; //�޸� ���� ��󿡼� ����

                    instance = obj.AddComponent<UIManager>();
                }
            }

            return instance;
        }
    }

    [Header("�����̴� ���� �̹���")]
    public Image bloodPackImg;
    public Image bloodBarImg;

    [Header("�ؽ�Ʈ")]
    public Text lifeText;
    public Text curBloodText;
    public Text maxBloodText;
    public Text barText;

    [Header("ĵ���� �׷�")]
    public CanvasGroup basicPanel;
    public CanvasGroup hiddenPanel;
    public CanvasGroup packImg;

    [Header("��� �гε�")]
    public Transform[] shakeTrms;
    public float force;
    public float duration;
    Sequence seq1;

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

    public void UpdateLifeText()
    {
        lifeText.text = string.Concat(GameManager.Instance.life + "/" + GameManager.Instance.maxLife);
    }

    public void UpdateBarText()
    {
        barText.text = string.Concat(Mathf.Round(GameManager.Instance.curBloodOnce), " / ", GameManager.Instance.maxBloodOnce);
    }

    public void UpdateCurBloodText()
    {
        curBloodText.text = GameManager.Instance.curBlood.ToString();
    }

    public void UpdateMaxBloodText()
    {
        maxBloodText.text = GameManager.Instance.maxBlood.ToString();
    }

    public void UpdatePackImg()
    {
        DOTween.To(() => bloodPackImg.fillAmount, x => bloodPackImg.fillAmount = x, GameManager.Instance.curBlood / GameManager.Instance.maxBlood, 1);
    }

    public void UpdateBarImg(float delay)
    {
        DOTween.To(() => bloodBarImg.fillAmount, x => bloodBarImg.fillAmount = x, GameManager.Instance.curBloodOnce / GameManager.Instance.maxBloodOnce, delay).SetEase(Ease.Linear);
    }

    public void UpdateBarImg()
    {
        bloodBarImg.fillAmount = GameManager.Instance.curBloodOnce / GameManager.Instance.maxBloodOnce;
    }

    public void ShakeCam()
    {
        seq1.Kill();

        seq1 = DOTween.Sequence();

        seq1.Append(shakeTrms[0].DOShakePosition(duration, force));

        for (int i = 1; i < shakeTrms.Length; i++)
        {
            seq1.Join(shakeTrms[i].DOShakePosition(duration, force));
        }
    }
}
