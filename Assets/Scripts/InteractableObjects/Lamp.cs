using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform Transform { get; private set; }


    private bool isEnabled;


    [SerializeField]
    private Light lampLight;


    private bool IsEnabled
    {
        get => isEnabled;
        set
        {
            // Enable/disable lamp.
            isEnabled = value;
            lampLight.gameObject.SetActive(value);
        }
    }


    private void Awake()
    {
        Transform = transform;
    }


    public void Interact()
    {
        IsEnabled = !IsEnabled;
    }
}