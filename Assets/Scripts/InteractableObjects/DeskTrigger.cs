using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable
{
    public event Action DeskTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    private void OnDestroy()
    {
        DeskTriggered = null;
    }

    public void Interact()
    {
        DeskTriggered?.Invoke();
    }
}