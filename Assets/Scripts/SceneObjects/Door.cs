using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private InteractionTrigger doorTrigger;

    [SerializeField]
    private Animator doorAnimator;

    [SerializeField]
    private bool isInverted = false;

    [SerializeField][Tooltip("True: door open\nFalse: door closed")]
    private bool defaultState = false;

    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";

    private bool isDoorOpen;


    private void Awake()
    {
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);

        doorTrigger.InteractVisual = GetComponent<InteractionVisual>();
    }

    private void Start()
    {
        doorTrigger.InteractionTriggered += () => SetDoorOpen(!isDoorOpen);
    }

    private void OnEnable()
    {
        SetDoorOpen(defaultState);
    }

    private void SetDoorOpen(bool open)
    {
        isDoorOpen = open;
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        doorAnimator.SetBool(DOOR_OPEN_BOOL, isDoorOpen);
    }
}
