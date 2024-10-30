using UnityEngine;

public class InteractableObjectTestNew : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; set; }

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    private void Start()
    {
        gameObject.layer = InteractionController.DefaultInteractableLayerIndex;
    }

    public virtual void Interact() { }
}
