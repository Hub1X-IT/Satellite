using UnityEngine;

public class InteractionController : MonoBehaviour {

    public static InteractionController Instance { get; private set; }


    // [SerializeField] private Transform cameraFollowObject;
    [SerializeField] private float interactRange;

    // public LayerMask DefaultInteractableLayerMask { get; private set; }
    // public LayerMask InteractableLayerMasks { get; private set; }
    

    [SerializeField][Tooltip("Only one should be selected!")] private LayerMask defaultInteractableLayerMask;
    [SerializeField] private LayerMask interactableLayerMasks;


    public Camera MainCamera { get; private set; } // optional - can use Camera.main


    private void Awake() {
        Instance = this;
        // DefaultInteractableLayerMask = defaultInteractableLayerMask;
        // InteractableLayerMasks = interactableLayerMasks;
        MainCamera = Camera.main;
    }


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (TryGetInteractableObject(out IInteractable interactableObject)) interactableObject.Interact();
    }   

    
    public bool TryGetInteractableObject(out IInteractable interactableObject) {
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit hit,
            interactRange, interactableLayerMasks)) {
            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null) return true;
        }
        interactableObject = null;
        return false;
    }

    /*
    public bool TryGetInteractableObject(out IInteractable interactableObject) {
        if (Physics.Raycast(cameraFollowObject.position, cameraFollowObject.forward, out RaycastHit hit,
            interactRange, InteractionData.Instance.InteractableLayerMasks)) {

            Debug.Log(hit.transform.gameObject); // !!!!! Debug log !!!!!!!!!!

            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null) return true;
        }
        interactableObject = null;
        return false;
    }
    */
}