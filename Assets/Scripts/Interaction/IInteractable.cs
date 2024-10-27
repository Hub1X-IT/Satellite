using UnityEngine;

public interface IInteractable
{
    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    public void Interact();
}