using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; }

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
