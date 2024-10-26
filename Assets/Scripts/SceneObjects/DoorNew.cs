using UnityEngine;

public class DoorNew : MonoBehaviour {

    DoorTrigger doorTrigger;
    [SerializeField] Animator doorAnimator;

    [SerializeField] private bool isInverted = false;

    private bool doorOpen;

    [SerializeField][Tooltip("checked = door open\nunchecked = door closed")] private bool defaultDoorState = false;


    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";


    private InteractionVisual interactionVisual;


    private void Awake() {
        doorTrigger = GetComponentInChildren<DoorTrigger>();
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        interactionVisual = GetComponent<InteractionVisual>();
    }


    private void Start() {
        doorTrigger.OnDoorInteract += () => { OpenDoor(!doorOpen); };
        doorTrigger.SetInteractionVisual(interactionVisual);
    }


    private void OnEnable() {
        doorOpen = defaultDoorState;
        OpenDoor(doorOpen);
    }

    public void OpenDoor(bool targetState) {
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        doorAnimator.SetBool(DOOR_OPEN_BOOL, targetState);
        doorOpen = targetState;
    }
}
