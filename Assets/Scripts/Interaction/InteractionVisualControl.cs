using UnityEngine;

public class InteractionVisualControl : MonoBehaviour {


    [SerializeField] private InteractionUI interactionUI;


    private InteractionVisual previousInteractVisual;


    // To make sure the methods don't run every frame
    private IInteractable previousInteractableObject;    
    private bool interactVisualEnabled;

        
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
        Debug.Log("InteractionVisualControl: ChangeInteractVisual()");

        DisableInteractVisual(previousInteractVisual);
        previousInteractVisual = null;       

        if (interactableObject.GetTransform().TryGetComponent<InteractionVisual>(out InteractionVisual interactVisual)) {
            EnableInteractVisual(interactVisual);
            previousInteractVisual = interactVisual;
        }

        previousInteractableObject = interactableObject;
    }


    private void EnableInteractVisual(InteractionVisual interactVisual) {
        Debug.Log("InteractionVisualControl: EnableInteractVisual()");
        interactVisual.EnableOutline();
        string interactMessage = interactVisual.GetInteractMessage();
        interactionUI.EnableInteractionText(interactMessage);

        interactVisualEnabled = true;
    }


    private void DisableInteractVisual(InteractionVisual interactVisual) {
        Debug.Log("InteractionVisualControl: DisableInteractVisual()");
        if (interactVisual != null) {
            interactVisual.DisableOutline();
        }
        
        interactionUI.DisableInteractionText();

        interactVisualEnabled = false;
    }
}