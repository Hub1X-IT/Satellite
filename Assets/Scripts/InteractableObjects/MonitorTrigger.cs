using System;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour, IInteractable {

    public event EventHandler OnMonitorInteract;

    private InteractionVisual interactionVisual;

    private void Start() {
        // gameObject.layer = InteractionController.Instance.DefaultInteractableLayerMask;
    }


    public void Interact() {
        OnMonitorInteract?.Invoke(this, EventArgs.Empty);
    }

    public void SetInteractionVisual(InteractionVisual interactionVisual) { this.interactionVisual = interactionVisual; }

    public InteractionVisual GetInteractionVisual() { return interactionVisual; }

    public Transform GetTransform() { return transform; }    
}
