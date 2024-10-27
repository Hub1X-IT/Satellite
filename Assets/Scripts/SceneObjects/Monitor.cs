using UnityEngine;

public class Monitor : MonoBehaviour
{
    private MonitorTrigger monitorTrigger;


    private Desk desk;


    [SerializeField]
    private Camera monitorUICamera;


    private bool isInMonitorView;
    private bool IsInMonitorView
    {
        get => isInMonitorView;
        set
        {
            // Enter/exit monitor view
            GameManager.IsCursorShown = value;

            PlayerScriptsController.CanShowPlayerHUD(!value);

            desk.CanExitDeskView = !value;
            desk.IsDeskCameraRotationEnabled = !value;

            if (value)
            {
                GameInput.PlayerInputActions.LaptopAndMonitor.Enable();
                CameraController.CurrentCamera = monitorUICamera;
            }
            else
            {
                GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
                CameraController.ChangeToMainCamera();
            }

            isInMonitorView = value;
        }
    }


    private bool isMonitorTriggerEnabled;
    public bool IsMonitorTriggerEnabled
    {
        get => isMonitorTriggerEnabled;
        set
        {
            monitorTrigger.gameObject.SetActive(value);
            isMonitorTriggerEnabled = value;
        }
    }


    public bool CanExitMonitorView { get; set; }


    private void Awake()
    {
        monitorTrigger = GetComponentInChildren<MonitorTrigger>();

        desk = GetComponentInParent<Desk>();

        monitorTrigger.InteractVisual = GetComponent<InteractionVisual>();

        IsMonitorTriggerEnabled = false;

        isInMonitorView = false;
        monitorUICamera.gameObject.SetActive(false);

        CanExitMonitorView = true;
    }


    private void Start()
    {
        monitorTrigger.OnMonitorInteract += () => IsInMonitorView = true;

        desk.DeskViewEnterExit += (bool state) => IsMonitorTriggerEnabled = state;

        GameInput.OnLaptopAndMonitorExitAction += () =>
        {
            if (CanExitMonitorView)
            {
                IsInMonitorView = false;
            }
        };
    }
}
