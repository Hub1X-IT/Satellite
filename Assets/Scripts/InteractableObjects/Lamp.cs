using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform SelfTransform { get; private set; }

    public bool IsLampEnabled { get; private set; }


    [SerializeField]
    private Light lampLight;


    private void Awake()
    {
        SelfTransform = transform;
    }


    public void Interact()
    {
        SetLampEnabled(!IsLampEnabled);
    }

    private void SetLampEnabled(bool enabled)
    {
        IsLampEnabled = enabled;
        lampLight.gameObject.SetActive(enabled);
    }
}