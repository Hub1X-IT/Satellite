using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour {

    private PlayerInputActions playerInputActions;

    private DeskTrigger deskTrigger;

    public event Action<bool> OnDeskViewEnterExit;


    private CameraRotationController deskCameraRotationController;
    private Vector3 deskCameraDefaultRotation = new Vector3(0f, 180f, 0f);


    [SerializeField] private CinemachineCamera cinemachineDeskCamera;


    private InteractionVisual interactionVisual;


    public bool CanExitDeskView { get; set; }


    private void Awake() {
        deskTrigger = GetComponentInChildren<DeskTrigger>();

        deskCameraRotationController = GetComponent<CameraRotationController>();
        deskCameraRotationController.enabled = false;

        interactionVisual = GetComponent<InteractionVisual>();

        cinemachineDeskCamera.gameObject.SetActive(false);

        CanExitDeskView = true;
    }


    private void Start() {
        deskTrigger.OnDeskTrigger += DeskTrigger_OnDeskTrigger;
        deskTrigger.SetInteractionVisual(interactionVisual);

        GameInput.OnExitDeskViewAction += GameInput_OnExitDeskViewAction;

        playerInputActions = GameInput.GetInputActions();
    }


    private void DeskTrigger_OnDeskTrigger() {
        EnterDeskView();
    }


    private void GameInput_OnExitDeskViewAction() {
        if (CanExitDeskView) ExitDeskView();
    }


    public void EnableDeskCameraRotationController(bool targetState) {
        deskCameraRotationController.enabled = targetState;
    }


    private void EnterDeskView() {
        deskTrigger.gameObject.SetActive(false); // disable desk triggerbox

        playerInputActions.PlayerWalking.Disable();

        PlayerScriptsController.EnablePlayerMovement(false);

        CameraController.ChangeCinemachineCamera(cinemachineDeskCamera);

        deskCameraRotationController.enabled = true;
        // Reset camera rotation
        // deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);

        playerInputActions.Desk.Enable();

        OnDeskViewEnterExit?.Invoke(true);
    }


    private void ExitDeskView() {
        playerInputActions.Desk.Disable();

        deskCameraRotationController.enabled = false;

        CameraController.Instance.ChangeToCinemachineMainCamera();

        PlayerScriptsController.EnablePlayerMovement(true);

        playerInputActions.PlayerWalking.Enable();

        deskTrigger.gameObject.SetActive(true);

        OnDeskViewEnterExit?.Invoke(false);
    }
}
