using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractVisualControl : MonoBehaviour {


    [SerializeField] private InteractionUI interactionUI;


    private InteractVisual previousInteractVisual;


    // To make sure you the methods don't run every frame
    private Transform previousTransform;    
    private bool interactVisualEnabled;


    private void Update() {
        CheckForInteraction();
    }


    private void CheckForInteraction() {

        if (Physics.Raycast(PlayerInteractData.Instance.cameraFollowObject.position, PlayerInteractData.Instance.cameraFollowObject.forward, out RaycastHit hit,
            PlayerInteractData.Instance.interactRange, PlayerInteractData.Instance.interactableLayerMasks)) {

            if (hit.transform != previousTransform) {
                ChangeInteractVisuals(hit);
            }
        }
        else {
            if (interactVisualEnabled) {
                DisableInteractVisual(previousInteractVisual);
                previousInteractVisual = null;
                previousTransform = null;
            }
        }
    }


    private void ChangeInteractVisuals(RaycastHit hit) {

        DisableInteractVisual(previousInteractVisual);
        previousInteractVisual = null;

        InteractVisual interactVisual;

        if (hit.transform.TryGetComponent<InteractVisual>(out interactVisual)) {
            EnableInteractVisual(interactVisual);
            previousInteractVisual = interactVisual;
        }

        previousTransform = hit.transform;
    }


    private void EnableInteractVisual(InteractVisual interactVisual) {
        interactVisual.EnableOutline();
        string interactMessage = interactVisual.GetInteractMessage();
        interactionUI.EnableInteractionText(interactMessage);

        interactVisualEnabled = true;
    }


    private void DisableInteractVisual(InteractVisual interactVisual) {
        if (interactVisual != null) {
            interactVisual.DisableOutline();
        }
        interactionUI.DisableInteractionText();

        interactVisualEnabled = false;
    }
}