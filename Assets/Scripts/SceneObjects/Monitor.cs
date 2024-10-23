using UnityEngine;

public class Monitor : MonoBehaviour {

    private PlayerInputActions playerInputActions;

    private MonitorTrigger monitorTrigger;

    private Desk desk;

    private bool isInMonitorView;

    private const CameraController.Cameras MONITOR_CAMERA = CameraController.Cameras.MonitorUICamera;

    private InteractionVisual interactionVisual;

    public bool CanExitMonitorView { get; set; }

    private void Awake() {
        monitorTrigger = GetComponentInChildren<MonitorTrigger>();

        desk = GetComponentInParent<Desk>();

        interactionVisual = GetComponent<InteractionVisual>();

        CanExitMonitorView = true;
    }

    private void Start() {
        monitorTrigger.OnMonitorInteract += MonitorTrigger_OnMonitorInteract;
        monitorTrigger.SetInteractionVisual(interactionVisual);

        playerInputActions = GameInput.Instance.GetInputActions();

        GameInput.Instance.OnLaptopAndMonitorExitAction += GameInput_OnLaptopAndMonitorExitAction;

        isInMonitorView = false;
    }
        

    private void MonitorTrigger_OnMonitorInteract(object sender, System.EventArgs e) {
        EnterMonitorView();
    }


    private void GameInput_OnLaptopAndMonitorExitAction(object sender, System.EventArgs e) {
        if (CanExitMonitorView) ExitMonitorView();
    }


    public void EnableMonitorTrigger(bool targetState) {
        monitorTrigger.gameObject.SetActive(targetState);
    }


    private void EnterMonitorView() {
        isInMonitorView = true;

        desk.CanExitDeskView = false;
        desk.EnableDeskCameraRotationController(false);

        CameraController.Instance.SetActiveCamera(MONITOR_CAMERA);
        GameManager.Instance.ShowCrosshair(false);
        GameManager.Instance.ShowCursor(true);

        playerInputActions.LaptopAndMonitor.Enable();
    }


    private void ExitMonitorView() {
        playerInputActions.LaptopAndMonitor.Disable();        
        
        GameManager.Instance.ShowCursor(false);
        GameManager.Instance.ShowCrosshair(true);
        CameraController.Instance.SetActiveCamera(CameraController.Cameras.MainCamera);

        desk.EnableDeskCameraRotationController(true);
        desk.CanExitDeskView = true;

        isInMonitorView = false;
    }
}
