using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FactoryPanel : MonoBehaviour
{
    public Text text;
    public Button touchArea;

    [Header("추가 에니메이션")]
    public Transform moImg;
    public Transform originTrm;
    public Transform target;
    public float duration;

    [SerializeField]
    private int clickCount = 0;

    public Parts[] parts;

    Sequence seq;

    void Start()
    {
        touchArea.onClick.AddListener(AddCount);

        foreach (var part in parts)
        {
            part.ResetPart();
        }
    }

    private void AddCount()
    {
        if (100 < clickCount + GameManager.Instance.clickAmount)
        {
            GameManager.Instance.AddLife(1);

            DOTween.KillAll();

            foreach (var part in parts)
            {
                part.ResetPart();
            }

            moImg.position = originTrm.position;
            moImg.localScale = new Vector3(1, 1, 1);

            clickCount = 0;
            text.text = clickCount.ToString();

            seq = DOTween.Sequence();

            seq.Append(moImg.DOMove(target.position, duration));
            seq.Join(moImg.DOScale(0, duration));
        }
        else
        {
            clickCount += GameManager.Instance.clickAmount;
            text.text = clickCount.ToString();

            if(clickCount >= 16)
            {
                parts[0].MergePart();
            }
            if(clickCount >= 32)
            {
                parts[1].MergePart();
            }
            if(clickCount >= 48)
            {
                parts[2].MergePart();
            }
            if(clickCount >= 64)
            {
                parts[3].MergePart();
            }
            if(clickCount >= 78)
            {
                parts[4].MergePart();
            }
            if(clickCount >= 96)
            {
                parts[5].MergePart();
            }

        }
    }
}
