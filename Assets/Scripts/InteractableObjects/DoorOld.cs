using UnityEngine;

public class DoorOld : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator doorAnimator;

    private const string DOOR_OPEN_TRIGGER = "DoorOpen";
    private const string DOOR_CLOSE_TRIGGER = "DoorClose";

    private bool isDoorOpen;

    public InteractionVisual InteractVisual { get; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;

        doorAnimator = GetComponent<Animator>();
        isDoorOpen = false;
    }


    public void Interact()
    {
        OpenClose();
    }


    private void OpenClose()
    {
        if (!isDoorOpen)
        {
            doorAnimator.SetTrigger(DOOR_OPEN_TRIGGER);
        }
        else
        {
            doorAnimator.SetTrigger(DOOR_CLOSE_TRIGGER);
        }
        isDoorOpen = !isDoorOpen;
    }
}
