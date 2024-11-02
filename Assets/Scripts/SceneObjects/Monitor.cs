using System;
using Unity.Cinemachine;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public event Action<bool> MonitorViewSetActive;

    [SerializeField]
    private InteractionTrigger monitorTrigger;

    private Desk desk;

    [SerializeField]
    private CinemachineCamera monitorCinemachineCamera;

    // Probably a temporary solution
    private Outline outline;

    // public bool IsMonitorViewActive { get; private set; }

    // public bool IsMonitorTriggerEnabled { get; private set; }

    public bool CanExitMonitorView { get; set; }
    

    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        monitorTrigger.InteractVisual = GetComponent<InteractionVisual>();

        monitorTrigger.InteractionTriggered += () => SetMonitorViewActive(true);
        desk.DeskViewEnabled += SetMonitorTriggerEnabled;

        GameInput.OnLaptopAndMonitorExitAction += () =>
        {
            if (CanExitMonitorView)
            {
                SetMonitorViewActive(false);
            }
        };

        // IsMonitorViewActive = false;

        monitorCinemachineCamera.enabled = false;

        SetMonitorTriggerEnabled(false);

        CanExitMonitorView = true;
    }

    private void OnDestroy()
    {
        MonitorViewSetActive = null;
    }

    private void SetMonitorViewActive(bool active)
    {
        // IsMonitorViewActive = active;

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        desk.CanExitDeskView = !active;
        desk.SetDeskCameraRotationEnabled(!active);

        SetMonitorTriggerEnabled(!active);

        // Probably a temporary solution
        outline.enabled = !active;

        if (active)
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Enable();
            CameraController.SetActiveCinemachineCamera(monitorCinemachineCamera);
        }
        else
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
            CameraController.SetActiveCinemachineCamera(desk.DeskCinemachineCamera);
        }

        MonitorViewSetActive?.Invoke(active);
    }

    private void SetMonitorTriggerEnabled(bool enabled)
    {
        // IsMonitorTriggerEnabled = enabled;
        monitorTrigger.gameObject.SetActive(enabled);
    }
}
