using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [Header("������ �ʿ��� ĵ����")]
    public RectTransform originTrm;

    [Header("�г�")]
    public RectTransform[] panels = new RectTransform[5];
    private Vector2[] minusPos = new Vector2[5];

    [Header("��ư��")]
    public Button[] btns = new Button[5];

    [Header("�г� �����̴� �ӵ�")]
    public float speed = 0.5f;

    public int curPanal = 2; //�߽�(2��) �гο��� ����
    private int beforePanal;

    private RectTransform rect; //�θ��� RectTransform

    private bool isComplete = true; //�г� �����̴°� ��������

    private float w;
    private float h;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        #region UI ũ��, ��ġ ����
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
        if(targetPanel != curPanal) //��ǥ �г��� ���� �гΰ� �ٸ��� ��Ʈ���� ������ ��
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
