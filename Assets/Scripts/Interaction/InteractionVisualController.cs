using UnityEngine;

public class InteractionVisualController : MonoBehaviour {

    // should be put in the HUD object

    private InteractionUI interactionUI;


    private InteractionVisual previousInteractVisual;


    // To make sure the methods don't run every frame
    private IInteractable previousInteractableObject;    
    private bool interactVisualEnabled;

    private void Awake() {
        interactionUI = GetComponentInChildren<InteractionUI>();
    }

    private void Update() {
        CheckForInteraction();
    }


    private void CheckForInteraction() {
        if (InteractionController.Instance.TryGetInteractableObject(out IInteractable interactableObject)) {
            if (interactableObject != previousInteractableObject) {
                ChangeInteractVisuals(interactableObject);
            }
        }
        else {
            if (interactVisualEnabled) {
                DisableInteractVisual(previousInteractVisual);
                previousInteractVisual = null;
                previousInteractableObject = null;
            }
        }
    }
    

    private void ChangeInteractVisuals(IInteractable interactableObject) {
        DisableInteractVisual(previousInteractVisual);
        previousInteractVisual = null;       

        InteractionVisual interactVisual = interactableObject.GetInteractionVisual();
        if (interactVisual != null) {
            EnableInteractVisual(interactVisual);
            previousInteractVisual = interactVisual;
        }

        previousInteractableObject = interactableObject;
    }


    private void EnableInteractVisual(InteractionVisual interactVisual) {
        interactVisual.EnableOutline(true);
        string interactMessage = interactVisual.GetInteractMessage();
        interactionUI.EnableInteractionText(interactMessage);

        interactVisualEnabled = true;
    }


    private void DisableInteractVisual(InteractionVisual interactVisual) {
        if (interactVisual != null) {
            interactVisual.EnableOutline(false);
        }
        
        interactionUI.DisableInteractionText();

        interactVisualEnabled = false;
    }
}