using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Parts : MonoBehaviour
{
    private Image img;

    [SerializeField]
    private Vector3 mergedPos;
    [SerializeField]
    private Vector3 spreadPos;

    private void Awake()
    {
        img = GetComponent<Image>();
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
        transform.position = spreadPos;
    }

    public void MergePart()
    {
        img.enabled = true;

        transform.DOMove(mergedPos, 1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            UIManager.Instance.ShakeCam();
        });
    }
}
