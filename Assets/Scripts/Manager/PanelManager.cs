using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [Header("보정에 필요한 캔버스")]
    public RectTransform originTrm;

    [Header("패널")]
    public RectTransform[] panels = new RectTransform[5];
    private Vector2[] minusPos = new Vector2[5];

    [Header("버튼들")]
    public Button[] btns = new Button[5];

    [Header("패널 움직이는 속도")]
    public float speed = 0.5f;

    public int curPanal = 2; //중심(2번) 패널에서 시작
    private int beforePanal;

    private RectTransform rect; //부모의 RectTransform

    private bool isComplete = true; //패널 움직이는게 끝났는지

    private float w;
    private float h;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        #region UI 크기, 위치 보정
        w = originTrm.rect.width;
        h = originTrm.rect.height;

        minusPos[0].x = w * 2;
        minusPos[1].x = w;
        minusPos[2].x = 0;
        minusPos[3].x = -w;
        minusPos[4].x = -(w * 2);

        for (int i = 0; i < 5; i++)
        {
            panels[i].sizeDelta = new Vector2(w, h);
            panels[i].anchoredPosition = originTrm.anchoredPosition;
            panels[i].anchoredPosition -= minusPos[i];
        }
        #endregion

        btns[0].onClick.AddListener(() =>
        {
            MovePanel(0);
        });
        btns[1].onClick.AddListener(() =>
        {
            MovePanel(1);
        });
        btns[2].onClick.AddListener(() =>
        {
            MovePanel(2);
        });
        btns[3].onClick.AddListener(() =>
        {
            MovePanel(3);
        });
        btns[4].onClick.AddListener(() =>
        {
            MovePanel(4);
        });

        SetBtnColor(2);
    }

    private void MovePanel(int targetPanel)
    {
        if(targetPanel != curPanal) //목표 패널이 현재 패널과 다르고 닷트윈이 끝났을 때
        {
            if(isComplete)
            {
                isComplete = false;

                int temp = targetPanel - curPanal;
                beforePanal = curPanal;

                SetBtnColor(targetPanel);

                DOTween.To(() => rect.anchoredPosition, pos => rect.anchoredPosition = pos, rect.anchoredPosition - new Vector2(w * temp, 0), speed).OnComplete(() =>
                {
                    isComplete = true;
                    curPanal = targetPanel;
                });
            }
        }
    }

    private void SetBtnColor(int num)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == num)
            {
                btns[i].gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                continue;
            }

            btns[i].gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        }
    }
}
