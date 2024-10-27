using UnityEngine;

public class InteractionVisualController : MonoBehaviour
{
    private InteractionUI interactionUI;


    private InteractionVisual previousInteractVisual;


    // To make sure the methods don't run every frame
    private IInteractable previousInteractableObject;


    private void Awake()
    {
        interactionUI = GetComponent<InteractionUI>();
    }


    private void Update()
    {
        CheckForInteraction();
    }


    private void CheckForInteraction()
    {
        if (InteractionController.TryGetInteractableObject(out IInteractable interactableObject))
        {
            if (interactableObject != previousInteractableObject)
            {
                ChangeInteractVisual(interactableObject);
                previousInteractableObject = interactableObject;
            }
        }
        else
        {
            if (previousInteractVisual != null && previousInteractVisual.IsEnabled)
            {
                ChangeInteractVisual(null);
                previousInteractableObject = null;
            }
        }
    }


    private void ChangeInteractVisual(IInteractable interactableObject)
    {
        EnableDisableInteractVisual(previousInteractVisual, false);
        previousInteractVisual = null;

        if (interactableObject != null && interactableObject.InteractVisual != null)
        {
            EnableDisableInteractVisual(interactableObject.InteractVisual, true);
            previousInteractVisual = interactableObject.InteractVisual;
        }
    }


    private void EnableDisableInteractVisual(InteractionVisual interactVisual, bool state)
    {
        if (interactVisual != null)
        {
            interactVisual.IsEnabled = state;
        }

        if (state)
        {
            string interactMessage = interactVisual.InteractMessage;
            interactionUI.InteractionText = interactMessage;
        }

        interactionUI.IsInteractionTextEnabled = state;
    }
}