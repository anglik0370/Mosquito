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
    private int i = 1;

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
        if (100 <= clickCount + GameManager.Instance.clickAmount)
        {
            GameManager.Instance.AddLife(1);

            foreach (var part in parts)
            {
                part.ResetPart();
            }

            moImg.position = originTrm.position;
            moImg.localScale = new Vector3(1, 1, 1);

            clickCount = 0;
            text.text = clickCount.ToString();

            i = 1;

            seq = DOTween.Sequence();

            seq.Kill();

            seq.Append(moImg.DOMove(target.position, duration));
            seq.Join(moImg.DOScale(0, duration));
            seq.AppendCallback(() =>
            {
                UIManager.Instance.ShakeCam();
            });
        }
        else
        {
            clickCount += GameManager.Instance.clickAmount;
            text.text = clickCount.ToString();

            if (clickCount % ((float)i * 15) == 0)
            {
                //터치 수 15마다 해줘야 할 일
                parts[i-1].MergePart();

                i++;
            }
        }
    }
}
