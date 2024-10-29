using System;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour, IInteractable
{
    public event Action MonitorTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; private set; }


    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        MonitorTriggered?.Invoke();
    }
}
