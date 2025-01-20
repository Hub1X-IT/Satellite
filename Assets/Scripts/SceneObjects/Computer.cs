using System;
using Unity.Cinemachine;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public event Action<bool> ComputerViewEnabled;

    private Desk desk;

    [SerializeField]
    private GameEventComputerSO computerViewEnabledGameEvent;

    [SerializeField]
    private GameEventSO computerViewDisabledGameEvent;

    [SerializeField]
    private InteractionTrigger computerTrigger;

    [SerializeField]
    private CinemachineCamera computerCinemachineCamera;

    private Outline outline;

    private bool isInComputerView;

    public bool CanExitComputerView { get; set; }

    public bool CanEnterComputerView { get; set; }

    public bool IsComputerEnabled { get; set; }


    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        computerTrigger.InteractVisual = GetComponent<InteractionVisual>();

        computerTrigger.InteractionTriggered += () => SetComputerViewActive(true);

        desk.DeskViewEnabled += (enabled) =>
        {
            CanEnterComputerView = enabled;
            ToggleComputerTrigger();
        };

        GameInput.OnComputerExitAction += () =>
        {
            if (isInComputerView && CanExitComputerView)
            {
                SetComputerViewActive(false);
            }
        };

        DetectionManager.DetectionOccured += () =>
        {
            if (isInComputerView)
            {
                SetComputerViewActive(false);
            }
        };

        computerCinemachineCamera.enabled = false;

        isInComputerView = false;

        CanExitComputerView = true;
        CanEnterComputerView = false;
        IsComputerEnabled = false;

        ToggleComputerTrigger();
    }

    private void SetComputerViewActive(bool active)
    {
        isInComputerView = active;
        GameManager.IsInScreenView = active;

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        desk.CanExitDeskView = !active;
        desk.SetDeskCameraRotationEnabled(!active);

        ToggleComputerTrigger();

        // Probably a temporary solution
        outline.enabled = !active;

        ComputerViewEnabled?.Invoke(active);

        if (active)
        {
            GameInput.PlayerInputActions.Computer.Enable();
            CameraController.SetActiveCinemachineCamera(computerCinemachineCamera);
            computerViewEnabledGameEvent.RaiseEvent(this);
        }
        else
        {
            GameInput.PlayerInputActions.Computer.Disable();
            CameraController.SetActiveCinemachineCamera(desk.DeskCinemachineCamera);
            computerViewDisabledGameEvent.TryRaiseEvent();
        }
    }

    public void ToggleComputerTrigger()
    {
        computerTrigger.gameObject.SetActive(!isInComputerView && CanEnterComputerView && IsComputerEnabled);
    }

    public void ExitComputerView()
    {
        // Method to be invoked by an event listener
        if (isInComputerView)
        {
            SetComputerViewActive(false);
        }
    }
}
