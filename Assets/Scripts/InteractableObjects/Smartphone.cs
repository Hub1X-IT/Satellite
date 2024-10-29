using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
