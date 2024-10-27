using UnityEngine;

public class DoorOld : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;


    private const string DOOR_OPEN_TRIGGER = "DoorOpen";
    private const string DOOR_CLOSE_TRIGGER = "DoorClose";


    private bool doorOpened;

    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    public void Awake()
    {
        Transform = transform;

        doorAnimator = GetComponent<Animator>();
        doorOpened = false;
    }


    public void Interact()
    {
        OpenClose();
    }


    private void OpenClose()
    {
        if (!doorOpened)
        { // open door
            doorAnimator.SetTrigger(DOOR_OPEN_TRIGGER);
        }

        else
        { // close door
            doorAnimator.SetTrigger(DOOR_CLOSE_TRIGGER);
        }
        doorOpened = !doorOpened;

    }
}
