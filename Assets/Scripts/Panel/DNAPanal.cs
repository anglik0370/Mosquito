using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAPanal : MonoBehaviour
{
    public DNA[] dnas;

    private void Start()
    {
        for (int i = 0; i < dnas.Length; i++)
        {
            DataManager.Instance.LoadDNAData(ref dnas[i], i);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            for (int i = 0; i < dnas.Length; i++)
            {
                DataManager.Instance.SaveDNAData(dnas[i], i);
            }
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < dnas.Length; i++)
        {
            DataManager.Instance.SaveDNAData(dnas[i], i);
        }
    }
}
