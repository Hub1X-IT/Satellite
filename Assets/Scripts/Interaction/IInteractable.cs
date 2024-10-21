using UnityEngine;

public interface IInteractable {
    

    public void Interact();

    public InteractionVisual GetInteractionVisual();

    public Transform GetTransform();

}