using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private Interactable interactionTrigger;

    private bool isLampEnabled;

    [SerializeField]
    private Light lampLight;

    public void Start()
    {
        interactionTrigger.OnInteractionTriggered += () => SetLampEnabled(!isLampEnabled);
    }

    private void SetLampEnabled(bool enabled)
    {
        isLampEnabled = enabled;
        lampLight.gameObject.SetActive(enabled);
    }
}