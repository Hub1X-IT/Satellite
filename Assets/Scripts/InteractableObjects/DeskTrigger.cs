using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable
{

    public event Action OnDeskTrigger;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        OnDeskTrigger?.Invoke();
    }
}