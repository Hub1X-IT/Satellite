using UnityEngine;

public class Monitor : MonoBehaviour {

    private PlayerInputActions playerInputActions;

    [SerializeField] MonitorTrigger monitorTrigger;

    private bool isInMonitorView;

    private const CameraController.Cameras MONITOR_CAMERA = CameraController.Cameras.MonitorUICamera;

    private InteractionVisual interactionVisual;

    private void Awake() {
        interactionVisual = GetComponent<InteractionVisual>();
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
        ExitMonitorView();
    }


    private void EnterMonitorView() {
        isInMonitorView = true;

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

        isInMonitorView = false;
    }

}
