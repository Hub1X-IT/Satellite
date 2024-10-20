using UnityEngine;

public class DoorNew : MonoBehaviour {

    [SerializeField] DoorTrigger doorTrigger;
    [SerializeField] Animator doorAnimator;


    [SerializeField] private bool isInverted = false;


    private bool doorOpen;

    [SerializeField][Tooltip("checked = door open\nunchecked = door closed")] private bool defaultDoorState = false;


    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";
    

    private void Awake() {
        // doorTrigger = GetComponentInChildren<DoorTrigger>();
        // doorAnimator = GetComponentInChildren<Animator>();
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
    }


    private void Start() {
        doorTrigger.OnDoorInteract += DoorTrigger_OnDoorInteract;
    }


    private void OnEnable() {
        doorOpen = defaultDoorState;
        DoorOpenClose(doorOpen);
    }


    private void DoorTrigger_OnDoorInteract(object sender, System.EventArgs e) {
        DoorOpenClose(!doorOpen);
    }


    public void DoorOpenClose(bool targetState) {
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        doorAnimator.SetBool(DOOR_OPEN_BOOL, targetState);
        doorOpen = targetState;
    }
}
