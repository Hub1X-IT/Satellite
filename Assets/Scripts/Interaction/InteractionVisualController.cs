using UnityEngine;

public class InteractionVisualController : MonoBehaviour
{
    private InteractionUI interactionUI;

    private InteractionVisual previousInteractVisual;

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
        SetInteractVisualEnabled(previousInteractVisual, false);
        previousInteractVisual = null;

        if (interactableObject != null && interactableObject.InteractVisual != null)
        {
            SetInteractVisualEnabled(interactableObject.InteractVisual, true);
            previousInteractVisual = interactableObject.InteractVisual;
        }
    }

    private void SetInteractVisualEnabled(InteractionVisual interactVisual, bool enabled)
    {
        if (interactVisual != null)
        {
            interactVisual.SetInteractionVisualEnabled(enabled);
        }

        if (enabled)
        {
            string interactMessage = interactVisual.InteractMessage;
            interactionUI.SetInteractionText(interactMessage);
        }

        interactionUI.SetInteractionTextEnabled(enabled);
    }
}