using UnityEngine;

public class Desk : MonoBehaviour {


    private PlayerInputActions playerInputActions;


    [SerializeField] private DeskTrigger deskTrigger;


    private CameraRotationController deskCameraRotationController;
    private Vector3 deskCameraDefaultRotation = new Vector3(0f, 180f, 0f);


    private const CameraController.CinemachineCameras DESK_CAMERA = CameraController.CinemachineCameras.CinemachineDeskCamera;


    private InteractionVisual interactionVisual;


    private void Awake() {
        // deskTrigger = GetComponentInChildren<DeskTrigger>();

        deskCameraRotationController = GetComponent<CameraRotationController>();
        deskCameraRotationController.enabled = false;

        interactionVisual = GetComponent<InteractionVisual>();
    }


    private void Start() {
        GameInput.Instance.OnExitDeskViewAction += GameInput_OnExitDeskViewAction;

        deskTrigger.OnDeskTrigger += DeskTrigger_OnDeskTrigger;
        deskTrigger.SetInteractionVisual(interactionVisual);

        playerInputActions = GameInput.Instance.GetInputActions();
    }

    private void GameInput_OnExitDeskViewAction(object sender, System.EventArgs e) {
        ExitDeskView();
    }

    private void DeskTrigger_OnDeskTrigger(object sender, System.EventArgs e) {
        EnterDeskView();
    }


    private void EnterDeskView() {
        deskTrigger.gameObject.SetActive(false); // disable the desk triggerbox

        playerInputActions.PlayerWalking.Disable();

        PlayerScriptsController.Instance.EnablePlayerMovementController(false);
        PlayerScriptsController.Instance.EnablePlayerCameraRotationController(false);

        CameraController.Instance.SetActiveCinemachineCamera(DESK_CAMERA);

        deskCameraRotationController.enabled = true;
        // Reset camera rotation
        deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);

        playerInputActions.Desk.Enable();
    }


    private void ExitDeskView() {
        Debug.Log("Desk: ExitDeskView()");

        playerInputActions.Desk.Disable();
                
        deskCameraRotationController.enabled = false;

        CameraController.Instance.SetActiveCinemachineCamera(CameraController.CinemachineCameras.CinemachineMainCamera);

        PlayerScriptsController.Instance.EnablePlayerMovementController(true);
        PlayerScriptsController.Instance.EnablePlayerCameraRotationController(true);

        playerInputActions.PlayerWalking.Enable();

        deskTrigger.gameObject.SetActive(true);
    }
}
