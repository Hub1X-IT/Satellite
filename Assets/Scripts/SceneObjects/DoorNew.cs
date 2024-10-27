using UnityEngine;

public class DoorNew : MonoBehaviour
{
    private DoorTrigger doorTrigger;


    [SerializeField]
    private Animator doorAnimator;


    [SerializeField]
    private bool isInverted = false;


    private bool isOpen;
    private bool IsOpen
    {
        get => isOpen;
        set
        {
            // Open/close door.
            doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
            doorAnimator.SetBool(DOOR_OPEN_BOOL, value);
            isOpen = value;
        }
    }


    [SerializeField][Tooltip("True: door open\nFalse: door closed")]
    private bool defaultState = false;


    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";


    private void Awake()
    {
        doorTrigger = GetComponentInChildren<DoorTrigger>();
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);

        doorTrigger.InteractVisual = GetComponent<InteractionVisual>();
    }


    private void Start()
    {
        doorTrigger.OnDoorInteract += () => IsOpen = !IsOpen;
    }


    private void OnEnable()
    {
        IsOpen = defaultState;
    }
}
