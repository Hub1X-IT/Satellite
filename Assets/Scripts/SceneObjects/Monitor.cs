using UnityEngine;

public class Monitor : MonoBehaviour
{
    private MonitorTrigger monitorTrigger;

    private Desk desk;

    [SerializeField]
    private Camera monitorUICamera;


    public bool IsMonitorViewActive { get; private set; }

    public bool IsMonitorTriggerEnabled { get; private set; }

    public bool CanExitMonitorView { get; set; }


    private void Awake()
    {
        monitorTrigger = GetComponentInChildren<MonitorTrigger>();
        desk = GetComponentInParent<Desk>();
        monitorTrigger.InteractVisual = GetComponent<InteractionVisual>();

        monitorTrigger.MonitorTriggered += () => SetMonitorViewActive(true);
        desk.DeskViewEnabled += SetMonitorTriggerEnabled;

        GameInput.OnLaptopAndMonitorExitAction += () =>
        {
            if (CanExitMonitorView)
            {
                SetMonitorViewActive(false);
            }
        };

        IsMonitorViewActive = false;
        /*
        monitorUICamera.gameObject.SetActive(false);
        */
        monitorUICamera.enabled = false;

        SetMonitorTriggerEnabled(false);

        CanExitMonitorView = true;
    }


    private void SetMonitorViewActive(bool active)
    {
        IsMonitorViewActive = active;

        GameManager.SetCursorShown(active);

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        desk.CanExitDeskView = !active;
        desk.SetDeskCameraRotationEnabled(!active);

        if (active)
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Enable();
            CameraController.SetActiveCamera(monitorUICamera);
        }
        else
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
            CameraController.SetActiveCamera(CameraController.MainCamera);
        }
    }

    private void SetMonitorTriggerEnabled(bool enabled)
    {
        IsMonitorTriggerEnabled = enabled;
        monitorTrigger.gameObject.SetActive(enabled);
    }
}
