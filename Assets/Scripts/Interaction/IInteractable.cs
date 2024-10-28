using UnityEngine;

public interface IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform Transform { get; }

    public void Interact();
}