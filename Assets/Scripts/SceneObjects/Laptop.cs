using System;
using Unity.Cinemachine;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    public event Action<bool> LaptopViewSetActive;

    [SerializeField]
    private InteractionTrigger laptopTrigger;

    private Desk desk;

    [SerializeField]
    private CinemachineCamera laptopCinemachineCamera;

    private Outline outline;

    public bool CanExitLaptopView { get; set; }


    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        laptopTrigger.InteractVisual = GetComponent<InteractionVisual>();

        laptopTrigger.InteractionTriggered += () => SetLaptopViewActive(true);

        desk.DeskViewEnabled += SetLaptopTriggerEnabled;

        GameInput.OnLaptopAndMonitorExitAction += () =>
        {
            if (CanExitLaptopView)
            {
                SetLaptopViewActive(false);
            }
        };

        laptopCinemachineCamera.enabled = false;

        SetLaptopTriggerEnabled(false);

        CanExitLaptopView = true;
    }

    private void OnDestroy()
    {
        LaptopViewSetActive = null;
    }

    private void SetLaptopViewActive(bool active)
    {
        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        desk.CanExitDeskView = !active;
        desk.SetDeskCameraRotationEnabled(!active);

        SetLaptopTriggerEnabled(!active);

        // Probably a temporary solution
        outline.enabled = !active;

        if (active)
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Enable();
            GameInput.PlayerInputActions.CommandPrompt.Enable();
            CameraController.SetActiveCinemachineCamera(laptopCinemachineCamera);
        }
        else
        {
            GameInput.PlayerInputActions.LaptopAndMonitor.Disable();
            GameInput.PlayerInputActions.CommandPrompt.Disable();
            CameraController.SetActiveCinemachineCamera(desk.DeskCinemachineCamera);
        }

        LaptopViewSetActive?.Invoke(active);
    }

    private void SetLaptopTriggerEnabled(bool enabled)
    {
        laptopTrigger.gameObject.SetActive(enabled);
    }
}
