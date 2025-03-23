using System;
using UnityEngine;

public static class InteractionController
{
    [Serializable]
    public struct InitializationData
    {
        public float InteractRange;
        [Tooltip("Only one should be selected!")]
        public LayerMask DefaultInteractableLayerMask;
        public LayerMask InteractableLayerMasks;
        public LayerMask InteractionBlockingLayerMasks;
    }

    public static bool IsInteractionEnabled { get; set; }

    private static float interactRange;

    private static LayerMask defaultInteractableLayerMask;

    public static int DefaultInteractableLayerIndex { get; private set; }

    private static LayerMask interactableLayerMasks;

    private static LayerMask interactionBlockingLayerMasks;

    public static void OnAwake(InitializationData data)
    {
        interactRange = data.InteractRange;
        defaultInteractableLayerMask = data.DefaultInteractableLayerMask;
        interactableLayerMasks = data.InteractableLayerMasks;
        interactionBlockingLayerMasks = data.InteractionBlockingLayerMasks;

        DefaultInteractableLayerIndex = GetLayerIndex(defaultInteractableLayerMask.value);

        GameInput.OnInteractAction += () =>
        {
            if (IsInteractionEnabled && TryGetInteractableObject(out IInteractable interactableObject))
            {
                interactableObject.Interact();
            }
        };

        IsInteractionEnabled = true;
    }

    public static bool TryGetInteractableObject(out IInteractable interactableObject)
    {
        interactableObject = null;
        /*
        if (Physics.Raycast(CameraController.MainCamera.transform.position, CameraController.MainCamera.transform.forward,
            InteractRange, InteractionBlockingLayerMasks |= ~InteractableLayerMasks))
        {
            return false;
        }
        */
        if (Physics.Raycast(CameraController.MainCamera.transform.position, CameraController.MainCamera.transform.forward,
        out RaycastHit hit, interactRange, interactableLayerMasks | interactionBlockingLayerMasks))
        {
            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                return true;
            }
        }
        return false;
    }

    public static int GetLayerIndex(LayerMask layerMask)
    {
        /// Works properly only when just one layerMask is selected!
        return (int)Mathf.Log(layerMask.value, 2);
    }
}