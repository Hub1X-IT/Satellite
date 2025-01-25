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

    public static float InteractRange { get; private set; }

    public static LayerMask DefaultInteractableLayerMask { get; private set; }

    public static int DefaultInteractableLayerIndex { get; private set; }

    public static LayerMask InteractableLayerMasks { get; private set; }

    public static LayerMask InteractionBlockingLayerMasks { get; private set; }

    public static void OnAwake(InitializationData data)
    {
        InteractRange = data.InteractRange;
        DefaultInteractableLayerMask = data.DefaultInteractableLayerMask;
        InteractableLayerMasks = data.InteractableLayerMasks;
        InteractionBlockingLayerMasks = data.InteractionBlockingLayerMasks;

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
        out RaycastHit hit, InteractRange, InteractableLayerMasks | InteractionBlockingLayerMasks))
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