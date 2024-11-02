using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform SelfTransform { get; private set; }

    private bool isLampEnabled;

    [SerializeField]
    private Light lampLight;

    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        SetLampEnabled(!isLampEnabled);
    }

    private void SetLampEnabled(bool enabled)
    {
        isLampEnabled = enabled;
        lampLight.gameObject.SetActive(enabled);
    }
}