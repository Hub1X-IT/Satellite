using UnityEngine;

public class Smartphone : MonoBehaviour
{
    [SerializeField]
    private Interactable interactionTrigger;

    [SerializeField]
    private GameEventSO phonePickupGameEvent;

    private void Start()
    {
        interactionTrigger.OnInteractionTriggered += OnInteractionTriggered;
    }

    public void OnInteractionTriggered()
    {
        gameObject.SetActive(false);
        phonePickupGameEvent.TryRaiseEvent();
        PlayerScriptsController.Instance.SetCanShowSmartphoneUI(true);
    }
}
