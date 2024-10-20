using UnityEngine;

public class Desk : MonoBehaviour {


    private PlayerInputActions playerInputActions;


    [SerializeField] private DeskTrigger deskTrigger;


    private CameraRotationController deskCameraRotationController;
    private Vector3 deskCameraDefaultRotation = new Vector3(0f, 180f, 0f);


    private CameraController.Cameras deskCamera = CameraController.Cameras.DeskCamera;  


    private void Awake() {
        // deskTrigger = GetComponentInChildren<DeskTrigger>();

        deskCameraRotationController = GetComponent<CameraRotationController>();
        deskCameraRotationController.enabled = false;
    }


    private void Start() {
        deskTrigger.OnDeskTrigger += DeskTrigger_OnDeskTrigger;
        playerInputActions = GameInput.Instance.GetInputActions();
    }


    private void DeskTrigger_OnDeskTrigger(object sender, System.EventArgs e) {
        EnterDeskView();
    }


    private void EnterDeskView() {
        playerInputActions.PlayerWalking.Disable();

        CameraController.Instance.SetActiveCamera(deskCamera);        

        deskCameraRotationController.enabled = true;
        // Reset rotation; may be put in only one of the EnterDeskView and ExitDeskView methods.
        deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);
        /*
        done: change active input preset
        done: change to controlling desk camera instead of player camera
        */
    }


    private void ExitDeskView() {
        deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);
        deskCameraRotationController.enabled = false;

        CameraController.Instance.SetActiveCamera(CameraController.Cameras.MainCamera);

        playerInputActions.PlayerWalking.Enable();
    }
}
