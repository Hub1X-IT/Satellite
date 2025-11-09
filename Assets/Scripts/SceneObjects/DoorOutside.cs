using UnityEngine;

public class DoorOutside : MonoBehaviour
{
    [SerializeField]
    private InteractionTrigger interactionTrigger;
    [SerializeField]
    private InteractionVisual interactVisual;

    private void Awake()
    {
        interactionTrigger.InteractVisual = interactVisual;
    }
}