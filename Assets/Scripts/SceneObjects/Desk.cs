using UnityEngine;

public class Desk : MonoBehaviour {

    private PlayerInputActions playerInputActions;

    private DeskTrigger deskTrigger;

    private Monitor monitor;

    private PlayerScriptsController playerScriptsController;


    private CameraRotationController deskCameraRotationController;
    private Vector3 deskCameraDefaultRotation = new Vector3(0f, 180f, 0f);


    private const CameraController.CinemachineCameras DESK_CAMERA = CameraController.CinemachineCameras.CinemachineDeskCamera;


    private InteractionVisual interactionVisual;


    public bool CanExitDeskView { get; set; }


    private void Awake() {
        deskTrigger = GetComponentInChildren<DeskTrigger>();

        monitor = GetComponentInChildren<Monitor>();

        playerScriptsController = FindAnyObjectByType<PlayerScriptsController>();

        deskCameraRotationController = GetComponent<CameraRotationController>();
        deskCameraRotationController.enabled = false;

        interactionVisual = GetComponent<InteractionVisual>();

        CanExitDeskView = true;
    }


    private void Start() {
        deskTrigger.OnDeskTrigger += DeskTrigger_OnDeskTrigger;
        deskTrigger.SetInteractionVisual(interactionVisual);

        GameInput.Instance.OnExitDeskViewAction += GameInput_OnExitDeskViewAction;

        monitor.EnableMonitorTrigger(false);

        playerInputActions = GameInput.Instance.GetInputActions();
    }


    private void DeskTrigger_OnDeskTrigger(object sender, System.EventArgs e) {
        EnterDeskView();
    }


    private void GameInput_OnExitDeskViewAction(object sender, System.EventArgs e) {
        if (CanExitDeskView) ExitDeskView();
    }


    public void EnableDeskCameraRotationController(bool targetState) {
        deskCameraRotationController.enabled = targetState;
    }


    private void EnterDeskView() {
        deskTrigger.gameObject.SetActive(false); // disable the desk triggerbox

        playerInputActions.PlayerWalking.Disable();

        playerScriptsController.EnablePlayerMovement(false);

        CameraController.Instance.SetActiveCinemachineCamera(DESK_CAMERA);

        deskCameraRotationController.enabled = true;
        // Reset camera rotation
        // deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);

        monitor.EnableMonitorTrigger(true);

        playerInputActions.Desk.Enable();
    }


    private void ExitDeskView() {
        Debug.Log("Desk: ExitDeskView()");

        playerInputActions.Desk.Disable();

        monitor.EnableMonitorTrigger(false);
                
        deskCameraRotationController.enabled = false;

        CameraController.Instance.SetActiveCinemachineCamera(CameraController.CinemachineCameras.CinemachineMainCamera);

        playerScriptsController.EnablePlayerMovement(true);

        playerInputActions.PlayerWalking.Enable();

        deskTrigger.gameObject.SetActive(true);
    }
}
