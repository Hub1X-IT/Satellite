using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable
{
    [SerializeField]
    private InteractionVisual interactionVisual;

    public InteractionVisual InteractVisual => interactionVisual;

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
