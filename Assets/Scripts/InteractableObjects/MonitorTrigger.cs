using System;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour, IInteractable {

    public event Action OnMonitorInteract;

    private InteractionVisual interactionVisual;

    public void Interact() {
        OnMonitorInteract?.Invoke();
    }

    public void SetInteractionVisual(InteractionVisual interactionVisual) { this.interactionVisual = interactionVisual; }

    public InteractionVisual GetInteractionVisual() { return interactionVisual; }

    public Transform GetTransform() { return transform; }    
}
