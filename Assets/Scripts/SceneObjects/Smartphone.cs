using UnityEngine;

public class Smartphone : MonoBehaviour
{
    [SerializeField]
    private InteractionTrigger interactionTrigger;

    [SerializeField]
    private GameEventSO phonePickupGameEvent;

    private void Start()
    {
        interactionTrigger.OnInteractionTriggered += OnInteractionTriggered;
    }

    public void OnInteractionTriggered()
    {
        gameObject.SetActive(false);
        phonePickupGameEvent.RaiseEvent();
        PlayerScriptsController.Instance.SetCanShowSmartphone(true);
    }
}
