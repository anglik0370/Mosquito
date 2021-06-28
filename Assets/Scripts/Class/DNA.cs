using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNA : MonoBehaviour
{
    public int needLife;
    public int needBlood;

    public Text needLifeText;
    public Text needBloodText;

    public GameObject enableText;
    public GameObject needs;

    private Button btn;

    public GameObject lockImg;
    public bool isLock;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        if(isLock)
        {
            lockImg.SetActive(true);
            enableText.SetActive(false);
            needs.SetActive(true);
        }
        else
        {
            lockImg.SetActive(false);
            enableText.SetActive(true);
            needs.SetActive(false);
        }

        SetText();

        btn.onClick.AddListener(StartDNA);
    }

    private void SetText()
    {
        needLifeText.text = string.Concat("X", needLife);
        needBloodText.text = string.Concat("X", needBlood);
    }

    private void StartDNA()
    {
        if(isLock && GameManager.Instance.CheckLife(needLife) && GameManager.Instance.CheckBlood(needBlood))
        {
            GameManager.Instance.UseLife(needLife);
            GameManager.Instance.UseBlood(needBlood);

            lockImg.SetActive(false);
            enableText.SetActive(true);
            needs.SetActive(false);

            GameManager.Instance.beaSu += 1;

            isLock = false;
        }
    }
}
