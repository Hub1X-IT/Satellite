using UnityEngine;

public class InteractionData : MonoBehaviour {

    public static InteractionData Instance { get; private set; }

    public LayerMask DefaultInteractableLayerMask { get; private set; }
    public LayerMask InteractableLayerMasks { get; private set; }
    public Camera MainCamera { get; private set; }


    [SerializeField][Tooltip("Only one should be selected!")] private LayerMask defaultInteractableLayerMask;
    [SerializeField] private LayerMask interactableLayerMasks;


    private void Awake() {
        Instance = this;
        DefaultInteractableLayerMask = defaultInteractableLayerMask;
        InteractableLayerMasks = interactableLayerMasks;
        MainCamera = Camera.main;
    }
}