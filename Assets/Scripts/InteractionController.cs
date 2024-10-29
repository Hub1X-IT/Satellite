using System;
using UnityEngine;

public static class InteractionController
{
    [Serializable]
    public struct InitializationData
    {
        public float interactRange;
        [Tooltip("Only one should be selected!")]
        public LayerMask defaultInteractableLayerMask;
        public LayerMask interactableLayerMasks;
        public LayerMask interactionBlockingLayerMasks;
    }

    public static float InteractRange { get; private set; }

    public static LayerMask DefaultInteractableLayerMask { get; private set; }

    public static int DefaultInteractableLayerIndex { get; private set; }

    public static LayerMask InteractableLayerMasks { get; private set; }

    public static LayerMask InteractionBlockingLayerMasks { get; private set; }

    public static void InitializeOnAwake(InitializationData data)
    {
        InteractRange = data.interactRange;
        DefaultInteractableLayerMask = data.defaultInteractableLayerMask;
        InteractableLayerMasks = data.interactableLayerMasks;
        InteractionBlockingLayerMasks = data.interactionBlockingLayerMasks;

        DefaultInteractableLayerIndex = GetLayerIndex(DefaultInteractableLayerMask.value);

        GameInput.OnInteractAction += () =>
        {
            if (TryGetInteractableObject(out IInteractable interactableObject))
            {
                interactableObject.Interact();
            }
        };
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
        out RaycastHit hit, InteractRange, InteractableLayerMasks |= InteractionBlockingLayerMasks))
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