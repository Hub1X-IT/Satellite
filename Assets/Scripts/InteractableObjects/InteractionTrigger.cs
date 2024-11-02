using System;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour, IInteractable
{
    [SerializeField]
    private bool autoSetLayerOnStart = true;

    public event Action InteractionTriggered;

    public InteractionVisual InteractVisual { get; set; }

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        SelfTransform = transform;
    }

    private void Start()
    {
        if (autoSetLayerOnStart)
        {
            gameObject.layer = InteractionController.DefaultInteractableLayerIndex;
        }
    }

    private void OnDestroy()
    {
        InteractionTriggered = null;
    }

    public void Interact()
    {
        InteractionTriggered?.Invoke();
    }
}
