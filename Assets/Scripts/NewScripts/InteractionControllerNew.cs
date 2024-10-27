using UnityEngine;

public class InteractionControllerNew {

    private static Camera MainCamera { get; set; }

    public static float InteractRange { get; private set; }

    public static LayerMask DefaultInteractableLayerMask { get; private set; }
    public static LayerMask InteractableLayerMasks { get; private set; }

    public static void InitializeOnAwake(float interactRange, LayerMask defaultInteractableLayerMask, LayerMask interactableLayerMasks) {
        InteractRange = interactRange;
        DefaultInteractableLayerMask = defaultInteractableLayerMask;
        InteractableLayerMasks = interactableLayerMasks;
    }

    public static void InitializeOnStart() {
        GameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private static void GameInput_OnInteractAction() {
        if (TryGetInteractableObject(out IInteractable interactableObject)) interactableObject.Interact();
    }

    public static bool TryGetInteractableObject(out IInteractable interactableObject) {
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit hit, InteractRange, InteractableLayerMasks)) {
            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null) return true;
        }
        interactableObject = null;
        return false;
    }
}