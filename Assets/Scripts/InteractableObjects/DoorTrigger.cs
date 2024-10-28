using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    public event Action DoorTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        DoorTriggered?.Invoke();
    }
}
