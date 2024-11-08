using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    [SerializeField]
    private InteractionVisual interactionVisual;

    [SerializeField]
    private GameEvent phonePickup;

    public InteractionVisual InteractVisual => interactionVisual;

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        phonePickup.Raise();
        PlayerScriptsController.SetCanShowSmartphoneUI(true);
    }
}
