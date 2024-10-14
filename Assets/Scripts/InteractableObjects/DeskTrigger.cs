using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour, IInteractable {


    private CameraController.Cameras deskCamera = CameraController.Cameras.DeskCamera;

    private PlayerInputActions playerInputActions;


    private void Start() {
        playerInputActions = GameInput.Instance.GetInputActions();
    }


    public void Interact() {
        Debug.Log("Desk: Interact()");
        EnterDeskView();
    }


    public Transform GetTransform() { return transform; }


    private void EnterDeskView() {
        CameraController.Instance.SetActiveCamera(deskCamera);
        playerInputActions.PlayerWalking.Disable();
        // change active input preset
        // to do: change to controlling desk camera instead of player camera
    }


    private void ExitDeskView() {
        CameraController.Instance.SetActiveCamera(CameraController.Cameras.MainCamera);
        playerInputActions.PlayerWalking.Enable();
    }
}