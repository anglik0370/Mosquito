using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ErrorManager : MonoBehaviour
{
    private static ErrorManager instance;
    public static ErrorManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(ErrorManager)) as ErrorManager;
                if (!instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "ErrorManager";

                    obj.hideFlags = HideFlags.HideAndDontSave; //�޸� ���� ��󿡼� ����

                    instance = obj.AddComponent<ErrorManager>();
                }
            }

            return instance;
        }
    }

    public Text errorText;
    public CanvasGroup error;

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

    public void SendError(string payload)
    {
        Debug.Log(payload);

        errorText.text = payload;

        error.alpha = 1f;
        Invoke("CloseError", 0.5f);
    }

    private void CloseError()
    {
        error.alpha = 0f;
    }
}