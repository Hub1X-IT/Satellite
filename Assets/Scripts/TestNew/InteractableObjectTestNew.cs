using UnityEngine;

public class InteractableObjectTestNew : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        gameObject.layer = InteractionController.DefaultInteractableLayerIndex;
    }

    public virtual void Interact() { }
}
