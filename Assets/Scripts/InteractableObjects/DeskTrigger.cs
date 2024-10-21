using System;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable {

    public event EventHandler OnDeskTrigger;

    private InteractionVisual interactionVisual;

    public void Interact() {
        OnDeskTrigger?.Invoke(this, EventArgs.Empty);
    }

    public void SetInteractionVisual(InteractionVisual interactionVisual) { this.interactionVisual = interactionVisual; }

    public InteractionVisual GetInteractionVisual()  { return interactionVisual; }

    public Transform GetTransform() { return transform; }
}