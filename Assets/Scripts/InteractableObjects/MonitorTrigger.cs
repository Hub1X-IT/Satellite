using System;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour, IInteractable
{

    public event Action OnMonitorInteract;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        OnMonitorInteract?.Invoke();
    }
}
