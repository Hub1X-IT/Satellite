using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public bool IsInteractionEnabled { get; private set; }

    [SerializeField]
    private float interactRange;
    [SerializeField]
    private LayerMask defaultInteractableLayerMask;

    public static int DefaultInteractableLayerIndex { get; private set; }
    [SerializeField]
    private LayerMask interactableLayerMasks;
    [SerializeField]
    private LayerMask interactionBlockingLayerMasks;

    private void Awake()
    {
        DefaultInteractableLayerIndex = GetLayerIndex(defaultInteractableLayerMask.value);

        IsInteractionEnabled = true;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += OnInteractAction;

        GameManager.Instance.GamePausedUnpaused += (paused) =>
        {
            SetInteractionEnabled(!paused);
        };
    }

    private void OnInteractAction()
    {
        if (IsInteractionEnabled && TryGetInteractableObject(out InteractionTrigger interactableObject))
        {
            if (interactableObject.IsInteractable)
            {
                interactableObject.Interact();
            }
            else
            {
                DialogueManager.Instance.StartRandomInteractionNotNeededDialogue();
            }
        }
    }

    public bool TryGetInteractableObject(out InteractionTrigger interactableObject)
    {
        interactableObject = null;
        /*
        if (Physics.Raycast(CameraController.MainCamera.transform.position, CameraController.MainCamera.transform.forward,
            InteractRange, InteractionBlockingLayerMasks |= ~InteractableLayerMasks))
        {
            return false;
        }
        */
        if (Physics.Raycast(CameraController.Instance.MainCamera.transform.position, CameraController.Instance.MainCamera.transform.forward,
            out RaycastHit hit, interactRange, interactableLayerMasks | interactionBlockingLayerMasks))
        {
            interactableObject = hit.transform.GetComponent<InteractionTrigger>();
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

    public void SetInteractionEnabled(bool enabled)
    {
        IsInteractionEnabled = enabled;
    }
}