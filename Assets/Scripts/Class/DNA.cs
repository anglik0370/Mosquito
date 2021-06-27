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

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
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
        GameManager.Instance.UseLife(needLife);
        GameManager.Instance.UseBlood(needBlood);
    }
}
