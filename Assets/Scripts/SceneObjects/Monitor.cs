using UnityEngine;

public class Monitor : MonoBehaviour
{
    private MonitorTrigger monitorTrigger;

    private Desk desk;

    [SerializeField]
    private Camera monitorUICamera;


    private bool isInMonitorView;

    private bool isMonitorTriggerEnabled;


    public bool CanExitMonitorView { get; set; }


    private bool IsInMonitorView
    {
        get => isInMonitorView;
        set
        {
            // Enter/exit monitor view
            isInMonitorView = value;
            EnterExitMonitorView(value);
        }
    }


    public bool IsMonitorTriggerEnabled
    {
        get => isMonitorTriggerEnabled;
        set
        {
            // Enable/disable monitor trigger object
            isMonitorTriggerEnabled = value;
            monitorTrigger.gameObject.SetActive(value);
        }
    }

    private void Awake()
    {
        monitorTrigger = GetComponentInChildren<MonitorTrigger>();
        desk = GetComponentInParent<Desk>();
        monitorTrigger.InteractVisual = GetComponent<InteractionVisual>();

        monitorTrigger.MonitorTriggered += () => IsInMonitorView = true;
        desk.DeskViewEnterExit += (bool state) => IsMonitorTriggerEnabled = state;

        GameInput.OnLaptopAndMonitorExitAction += () =>
        {
            if (CanExitMonitorView)
            {
                IsInMonitorView = false;
            }
        };

        isInMonitorView = false;
        monitorUICamera.gameObject.SetActive(false);

        IsMonitorTriggerEnabled = false;

        CanExitMonitorView = true;
    }


    private void EnterExitMonitorView(bool state)
    {
        GameManager.IsCursorShown = state;

        PlayerScriptsController.CanShowPlayerHUD = !state;

        desk.CanExitDeskView = !state;
        desk.IsDeskCameraRotationEnabled = !state;

        if (state)
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Enable();
            CameraController.ActiveCamera = monitorUICamera;
        }
        else
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
            CameraController.ChangeToMainCamera();
        }
    }
}
