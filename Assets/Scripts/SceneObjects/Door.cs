using UnityEngine;

public class Door : MonoBehaviour
{
    private DoorTrigger doorTrigger;

    [SerializeField]
    private Animator doorAnimator;

    [SerializeField]
    private bool isInverted = false;

    [SerializeField][Tooltip("True: door open\nFalse: door closed")]
    private bool defaultState = false;


    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";


    public bool IsDoorOpen { get; private set; }


    private void Awake()
    {
        doorTrigger = GetComponentInChildren<DoorTrigger>();
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);

        doorTrigger.InteractVisual = GetComponent<InteractionVisual>();
    }

    private void Start()
    {
        doorTrigger.DoorTriggered += () => SetDoorOpen(!IsDoorOpen);
    }

    private void OnEnable()
    {
        SetDoorOpen(defaultState);
    }

    private void SetDoorOpen(bool open)
    {
        IsDoorOpen = open;
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        doorAnimator.SetBool(DOOR_OPEN_BOOL, open);
    }
}
