using UnityEngine;

public class Monitor : MonoBehaviour {

    private PlayerInputActions playerInputActions;

    private MonitorTrigger monitorTrigger;

    private Desk desk;

    private bool isInMonitorView;

    [SerializeField] private Camera monitorUICamera;

    private InteractionVisual interactionVisual;

    public bool CanExitMonitorView { get; set; }

    private void Awake() {
        monitorTrigger = GetComponentInChildren<MonitorTrigger>();

        desk = GetComponentInParent<Desk>();

        interactionVisual = GetComponent<InteractionVisual>();

        EnableMonitorTrigger(false);

        isInMonitorView = false;
        monitorUICamera.gameObject.SetActive(false);

        CanExitMonitorView = true;
    }

    private void Start() {
        monitorTrigger.OnMonitorInteract += () => { EnterMonitorView(); };
        monitorTrigger.SetInteractionVisual(interactionVisual);

        desk.OnDeskViewEnterExit += (bool state) => { EnableMonitorTrigger(state); };

        playerInputActions = GameInput.Instance.GetInputActions();

        GameInput.Instance.OnLaptopAndMonitorExitAction += () => { if (CanExitMonitorView) ExitMonitorView(); };
    }


    public void EnableMonitorTrigger(bool targetState) {
        monitorTrigger.gameObject.SetActive(targetState);
    }


    private void EnterMonitorView() {
        isInMonitorView = true;

        desk.CanExitDeskView = false;
        desk.EnableDeskCameraRotationController(false);

        CameraController.Instance.ChangeCamera(monitorUICamera);
        GameManager.Instance.ShowCursor(true);

        playerInputActions.LaptopAndMonitor.Enable();
    }


    private void ExitMonitorView() {
        playerInputActions.LaptopAndMonitor.Disable();        
        
        GameManager.Instance.ShowCursor(false);
        CameraController.Instance.ChangeToMainCamera();

        desk.EnableDeskCameraRotationController(true);
        desk.CanExitDeskView = true;

        isInMonitorView = false;
    }
}
