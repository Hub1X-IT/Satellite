using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotepaddAppCopyButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<bool> OnMouseOverButton;

    // May be temporary
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter" + eventData.position);
        OnMouseOverButton?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit" + eventData.position);
        OnMouseOverButton?.Invoke(false);
    }
}