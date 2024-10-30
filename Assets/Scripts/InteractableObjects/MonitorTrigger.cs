using System;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour, IInteractable
{
    public event Action MonitorTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform SelfTransform { get; private set; }


    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        MonitorTriggered?.Invoke();
    }
}
