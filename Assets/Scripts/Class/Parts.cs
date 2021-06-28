using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Parts : MonoBehaviour
{
    private Image img;

    private RectTransform rect;

    [SerializeField]
    private Vector2 mergedPos;
    [SerializeField]
    private Vector2 spreadPos;

    private bool isMerged = false;

    private void Awake()
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        
    }

    public bool isEnable()
    {
        if(img.enabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetPart()
    {
        img.enabled = false;
        rect.anchoredPosition = spreadPos;

        isMerged = false;
    }

    public void MergePart()
    {
        if (isMerged) return;

        img.enabled = true;

        rect.DOAnchorPos(mergedPos, 1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            UIManager.Instance.ShakeCam();
        });

        isMerged = true;
    }
}
