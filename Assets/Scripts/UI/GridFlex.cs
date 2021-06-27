using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridFlex : MonoBehaviour
{
    GridLayoutGroup gridLayoutGroup;
    RectTransform rectTransform;

    void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }


    void Start()
    {
        float w = rectTransform.rect.width;
        w = w / gridLayoutGroup.constraintCount;

        float h = w * 1.1f;

        gridLayoutGroup.cellSize = new Vector2(w, h);
    }
}
