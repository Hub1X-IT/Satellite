using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable
{
    public event Action DeskTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        DeskTriggered?.Invoke();
    }
}