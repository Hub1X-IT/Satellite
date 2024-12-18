using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    [SerializeField]
    private InteractionVisual interactionVisual;

    [SerializeField]
    private GameEventSO phonePickupGameEvent;

    public InteractionVisual InteractVisual => interactionVisual;

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        phonePickupGameEvent.TryRaiseEvent();
        PlayerScriptsController.SetCanShowSmartphoneUI(true);
    }
}
