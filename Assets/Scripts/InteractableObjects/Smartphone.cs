using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
