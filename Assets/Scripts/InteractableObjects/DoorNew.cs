using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNew : MonoBehaviour, IInteractable {

    [SerializeField] Animator doorAnimator;


    private const string DOOR_OPEN_BOOL = "DoorOpen";

    private bool doorOpen;

    [SerializeField][Tooltip("checked = door open\nunchecked = door closed")] private bool defaultDoorState = false;


    private void OnEnable() {
        doorOpen = defaultDoorState;
        OpenClose(doorOpen);
    }


    public void Interact() {
        OpenClose(!doorOpen);
    }


    public void OpenClose(bool targetState) {
        doorAnimator.SetBool(DOOR_OPEN_BOOL, targetState);
        doorOpen = targetState;
    }


    public Transform GetTransform() { return transform; }
}
