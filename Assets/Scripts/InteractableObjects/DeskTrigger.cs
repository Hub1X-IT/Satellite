using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable {

    public event Action OnDeskTrigger;

    private InteractionVisual interactionVisual;

    public void Interact() {
        OnDeskTrigger?.Invoke();
    }

    public void SetInteractionVisual(InteractionVisual interactionVisual) { this.interactionVisual = interactionVisual; }

    public InteractionVisual GetInteractionVisual()  { return interactionVisual; }

    public Transform GetTransform() { return transform; }
}