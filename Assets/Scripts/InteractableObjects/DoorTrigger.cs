using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    public event Action OnDoorInteract;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        OnDoorInteract?.Invoke();
    }
}
