using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    public event Action DoorTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        DoorTriggered?.Invoke();
    }
}
