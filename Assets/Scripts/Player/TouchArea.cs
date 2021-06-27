using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerInput pInput;

    public void OnPointerDown(PointerEventData eventData)
    {
        pInput.isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pInput.isTouch = false;
    }
}
