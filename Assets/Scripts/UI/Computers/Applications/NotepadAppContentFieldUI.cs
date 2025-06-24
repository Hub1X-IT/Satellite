using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotepadAppContentFieldUI : MonoBehaviour, IPointerClickHandler
{
    public event Action<Vector2> ContentFieldClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click " + eventData.pressPosition + " " + eventData.position);
        ContentFieldClicked?.Invoke(eventData.pressPosition);
    }
}