using UnityEngine;

public class InteractionController : MonoBehaviour {

    public static InteractionController Instance { get; private set; }


    [SerializeField] private Transform cameraFollowObject;
    [SerializeField] private float interactRange;


    private void Awake() {
        Instance = this;
    }


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        // Debug.Log("Interact"); // !!!!!!!!!!! Debug log !!!!!!!!!!!
        if (TryGetInteractableObject(out IInteractable interactableObject)) interactableObject.Interact();
    }   

    
    public bool TryGetInteractableObject(out IInteractable interactableObject) {
        if (Physics.Raycast(InteractionData.Instance.MainCamera.transform.position, InteractionData.Instance.MainCamera.transform.forward, out RaycastHit hit,
            interactRange, InteractionData.Instance.InteractableLayerMasks)) {
            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null) return true;
        }
        interactableObject = null;
        return false;
    }

    
    public bool TryGetInteractableObject_2(out IInteractable interactableObject) {
        if (Physics.Raycast(cameraFollowObject.position, cameraFollowObject.forward, out RaycastHit hit,
            interactRange, InteractionData.Instance.InteractableLayerMasks)) {

            Debug.Log(hit.transform.gameObject); // !!!!! Debug log !!!!!!!!!!

            interactableObject = hit.transform.GetComponent<IInteractable>();
            if (interactableObject != null) return true;
        }
        interactableObject = null;
        return false;
    }
}