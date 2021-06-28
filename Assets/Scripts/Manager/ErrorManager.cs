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

                    obj.hideFlags = HideFlags.HideAndDontSave; //메모리 해제 대상에서 제외

                    instance = obj.AddComponent<ErrorManager>();
                }
            }

            return instance;
        }
    }

    public Text errorText;
    public CanvasGroup error;

    private Coroutine co;

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

        if(co != null)
        {
            StopCoroutine(co);
        }

        co = StartCoroutine(errorRoutine());
    }

    private IEnumerator errorRoutine()
    {
        error.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        error.alpha = 0f;
    }
}
