using UnityEngine;

public class InteractionVisualController : MonoBehaviour
{
    [SerializeField]
    private InteractionUI interactionUI;
    [SerializeField]
    private InteractionController interactionController;

    private InteractionVisual previousInteractVisual;

    private Interactable previousInteractableObject;

    private void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (interactionController.TryGetInteractableObject(out Interactable interactableObject))
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

    private void ChangeInteractVisual(Interactable interactableObject)
    {
        // Debug.Log(interactableObject);

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