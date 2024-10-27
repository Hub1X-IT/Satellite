using System;
using UnityEngine;

public static class InteractionController
{
    [Serializable]
    public struct InitializationData
    {
        public float interactRange;
        [Tooltip("Only one should be selected!")] public LayerMask defaultInteractableLayerMask;
        [Tooltip("Select also the layers that interaction should not pass through")] public LayerMask interactableLayerMasks;
    }


    private static float interactRange;

    private static LayerMask defaultInteractableLayerMask;

    private static LayerMask interactableLayerMasks;

    public static float InteractRange => interactRange;

    public static LayerMask DefaultInteractableLayerMask => defaultInteractableLayerMask;

    public static LayerMask InteractableLayerMasks => interactableLayerMasks;

    public static void InitializeOnAwake(InitializationData data)
    {
        interactRange = data.interactRange;
        defaultInteractableLayerMask = data.defaultInteractableLayerMask;
        interactableLayerMasks = data.interactableLayerMasks;
    }

    public static void InitializeOnStart()
    {
        GameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private static void GameInput_OnInteractAction()
    {
        if (TryGetInteractableObject(out IInteractable interactableObject))
        {
            interactableObject.Interact();
        }
    }

    public static bool TryGetInteractableObject(out IInteractable interactableObject)
    {
        if (Physics.Raycast(CameraController.MainCamera.transform.position, CameraController.MainCamera.transform.forward,
            out RaycastHit hit, InteractRange, InteractableLayerMasks))
        {
            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                return true;
            }
        }
        interactableObject = null;
        return false;
    }
}