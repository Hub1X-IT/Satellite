using UnityEngine;

public interface IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform SelfTransform { get; }

    public void Interact();
}