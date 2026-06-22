using UnityEngine;

public class InteractionVisualController : MonoBehaviour
{
    [SerializeField]
    private InteractionUI interactionUI;
    [SerializeField]
    private InteractionController interactionController;

    private InteractionVisual previousInteractVisual;

    private InteractionTrigger previousInteractableObject;

    private void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (interactionController.TryGetInteractableObject(out InteractionTrigger interactableObject))
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

    private void ChangeInteractVisual(InteractionTrigger interactableObject)
    {
        DisableInteractVisual(previousInteractVisual);
        previousInteractVisual = null;

        if (interactableObject != null && interactableObject.InteractVisual != null)
        {
            EnableInteractVisual(interactableObject.InteractVisual);
            previousInteractVisual = interactableObject.InteractVisual;
        }
    }

    private void EnableInteractVisual(InteractionVisual interactVisual)
    {
        interactVisual.SetInteractionVisualEnabled(true);
        
        string interactMessage = interactVisual.InteractMessage;
        interactionUI.SetInteractionText(interactMessage);

        interactionUI.SetInteractionTextEnabled(true);
        interactionUI.SetInteractionIconEnabled(interactVisual.ShouldShowInteractionIcon);
    }

    private void DisableInteractVisual(InteractionVisual interactVisual)
    {
        if (interactVisual != null)
        {
            interactVisual.SetInteractionVisualEnabled(false);
        }

        interactionUI.SetInteractionTextEnabled(false);
        interactionUI.SetInteractionIconEnabled(false);
    }
}