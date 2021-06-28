using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private PlayerInput pInput;

    [Header("±âÅ¸ º¯¼ö")]
    [SerializeField] private float suckDelay = 1f;
    [SerializeField] private float deadPercent = 0.1f;

    private Coroutine co = null;
    private bool beforeTouch = false;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        UIManager.Instance.UpdateMaxBloodText();
    }

    private void Update()
    {
        if(beforeTouch != pInput.isTouch)
        {
            if (pInput.isTouch /*&& AnimManager.Instance.isAnimEnd*/ && GameManager.Instance.CheckLife(1))
            {
                SuckStart();
            }
            else
            {
                //´­·¶´Ù ¶­À» ¶§
                if(!GameManager.Instance.isGameOver)
                {
                    SuckEnd();
                }
                else
                {
                    GameManager.Instance.isGameOver = false;
                }
            }
        }

        beforeTouch = pInput.isTouch;
    }

    private void SuckStart()
    {
        GameManager.Instance.UseLife(1);

        if (co != null)
        {
            GameManager.Instance.curBloodOnce = 0;
            StopCoroutine(co);
        }

        co = StartCoroutine(SuckBloodRoutine());

        AnimManager.Instance.MosquitoMoveDown();

        AnimManager.Instance.FadeInCvs();
    }

    private void SuckEnd()
    {
        suckDelay = 1f;

        if(GameManager.Instance.curBloodOnce + GameManager.Instance.curBlood < GameManager.Instance.maxBlood)
        {
            GameManager.Instance.curBlood += GameManager.Instance.curBloodOnce * GameManager.Instance.beaSu;
        }
        else
        {
            GameManager.Instance.curBlood = GameManager.Instance.maxBlood;
        }

        GameManager.Instance.curBloodOnce = 0;

        DOTween.KillAll();

        UIManager.Instance.UpdateCurBloodText();
        UIManager.Instance.UpdateMaxBloodText();
        UIManager.Instance.UpdateBarImg();
        UIManager.Instance.UpdateBarText();
        UIManager.Instance.UpdatePackImg();

        AnimManager.Instance.MosquitoMoveUp();
        AnimManager.Instance.FadeOutCvs();

        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    private void MosquitoDead()
    {
        suckDelay = 1f;
        GameManager.Instance.curBloodOnce = 0;

        AnimManager.Instance.SwingArm();

        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    private IEnumerator SuckBloodRoutine()
    {
        UIManager.Instance.UpdateBarImg(suckDelay);
        UIManager.Instance.UpdateBarText();

        while (GameManager.Instance.curBloodOnce < GameManager.Instance.maxBloodOnce)
        {
            GameManager.Instance.curBloodOnce += GameManager.Instance.suckPerTime;

            yield return new WaitForSeconds(suckDelay);

            UIManager.Instance.UpdateBarImg(suckDelay);
            UIManager.Instance.UpdateBarText();

            if (Random.Range(0.0f, 10.0f) < deadPercent)
            {
                GameManager.Instance.isGameOver = true;
                MosquitoDead();

                yield break;
            }

            if (suckDelay >= 0.025f)
            {
                suckDelay = suckDelay * 0.8f;
            }
        }

        yield return null;
    }
}
