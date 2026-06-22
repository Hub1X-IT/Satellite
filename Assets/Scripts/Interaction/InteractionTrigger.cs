using System;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    private bool autoSetLayerOnStart = true;

    [SerializeField]
    private InteractionVisual interactionVisual;

    [SerializeField]
    private bool isInteractableOnStart = true;

    public event Action OnInteractionTriggered;

    public InteractionVisual InteractVisual => interactionVisual;

    public bool IsInteractable { get; private set; }

    private void Awake()
    {
        IsInteractable = isInteractableOnStart;
    }

    private void Start()
    {
        if (autoSetLayerOnStart)
        {
            gameObject.layer = InteractionController.DefaultInteractableLayerIndex;
        }
    }

    public void Interact()
    {
        OnInteractionTriggered?.Invoke();
    }

    public void SetObjectInteractable(bool interactable)
    {
        IsInteractable = interactable;
    }

    public void SetInteractionVisual(InteractionVisual interactVisual)
    {
        interactionVisual = interactVisual;
    }
}
