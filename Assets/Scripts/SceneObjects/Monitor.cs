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

    private Outline outline;

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
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
            CameraController.SetActiveCinemachineCamera(desk.DeskCinemachineCamera);
            GameManager.SetCursorShown(false);
        }

        MonitorViewSetActive?.Invoke(active);
    }

    private void SetMonitorTriggerEnabled(bool enabled)
    {
        monitorTrigger.gameObject.SetActive(enabled);
    }
}
