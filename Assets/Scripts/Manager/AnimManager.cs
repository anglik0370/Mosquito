using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimManager : MonoBehaviour
{
    private static AnimManager instance;
    public static AnimManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(AnimManager)) as AnimManager;
                if (!instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AnimManager";

                    obj.hideFlags = HideFlags.HideAndDontSave; //메모리 해제 대상에서 제외

                    instance = obj.AddComponent<AnimManager>();
                }
            }

            return instance;
        }
    }

    [Header("모기 움직임 관련")]
    public Transform mosquitoTrm;
    public Transform originTrm;
    public Transform sitTrm;

    [Header("팔 움직임 관련")]
    public Transform swingArmTrm;
    public Vector3 originPos;
    public Vector3 swingPos;

    [Header("죽었을 때 효과")]
    public Transform particleTrm;

    [Header("닷트윈 에니메이션이 끝났는지")]
    public bool isAnimEnd = true;

    private Sequence armSequence;
    private Sequence fadeSequence;

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

    public void FadeInCvs()
    {
        fadeSequence.Kill();
        fadeSequence = DOTween.Sequence();

        fadeSequence.Append(DOTween.To(() => UIManager.Instance.hiddenPanel.alpha, x => UIManager.Instance.hiddenPanel.alpha = x, 1, 0.5f));
        fadeSequence.Join(DOTween.To(() => UIManager.Instance.basicPanel.alpha, x => UIManager.Instance.basicPanel.alpha = x, 0, 1));
        fadeSequence.Join(DOTween.To(() => UIManager.Instance.packImg.alpha, x => UIManager.Instance.packImg.alpha = x, 0, 1));
    }

    public void FadeOutCvs()
    {
        fadeSequence.Kill();
        fadeSequence = DOTween.Sequence();

        fadeSequence.Append(DOTween.To(() => UIManager.Instance.hiddenPanel.alpha, x => UIManager.Instance.hiddenPanel.alpha = x, 0, 1));
        fadeSequence.Join(DOTween.To(() => UIManager.Instance.basicPanel.alpha, x => UIManager.Instance.basicPanel.alpha = x, 1, 0.5f));
        fadeSequence.Join(DOTween.To(() => UIManager.Instance.packImg.alpha, x => UIManager.Instance.packImg.alpha = x, 1, 0.5f));
    }

    public void MosquitoMoveUp()
    {
        mosquitoTrm.DOMove(originTrm.position, 1);
    }

    public void MosquitoMoveDown()
    {
        isAnimEnd = false;
        mosquitoTrm.DOMove(sitTrm.position, 1);
    }

    public void SwingArm()
    {
        fadeSequence.Kill();

        armSequence.Kill();
        armSequence = DOTween.Sequence();
        
        armSequence.Append(swingArmTrm.DORotate(swingPos, 0.5f, RotateMode.Fast));
        armSequence.Append(particleTrm.DOScale(new Vector3(1, 1, 1), 0.1f));
        armSequence.Append(particleTrm.DOScale(new Vector3(0, 0, 0), 0.1f));
        armSequence.AppendCallback(() =>
        {
            UIManager.Instance.ShakeCam();

            UIManager.Instance.UpdateCurBloodText();
            UIManager.Instance.UpdateMaxBloodText();
            UIManager.Instance.UpdateBarImg();
            UIManager.Instance.UpdateBarText();
            UIManager.Instance.UpdatePackImg();

            swingArmTrm.DORotate(originPos, 0.5f, RotateMode.Fast).OnComplete(() => 
            {
                DOTween.To(() => UIManager.Instance.basicPanel.alpha, x => UIManager.Instance.basicPanel.alpha = x, 1, 1f);
                FadeOutCvs();
                DOTween.To(() => UIManager.Instance.hiddenPanel.alpha, x => UIManager.Instance.hiddenPanel.alpha = x, 0, 0.5f).OnComplete(() =>
                {
                    mosquitoTrm.position = originTrm.position;
                });
            });
        }) ;
    }
}
