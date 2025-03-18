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

    private const float PlayerMovementEnableTimeOffset = 0.6f;

    private bool shouldEnablePlayerMovement;

    private float playerMovementEnableTimer;

    public bool CanExitComputerView { get; set; }

    private bool isComputerEnabled;


    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        computerTrigger.InteractVisual = GetComponent<InteractionVisual>();

        computerTrigger.InteractionTriggered += () => SetComputerViewActive(true);

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
        isComputerEnabled = false;

        shouldEnablePlayerMovement = false;

        ToggleComputerTrigger();
    }

    private void Start()
    {
        ServerConnectionManager.ServerConnectionEnabled += (enabled) =>
        {
            isComputerEnabled = enabled;
            ToggleComputerTrigger();
        };
    }

    private void Update()
    {
        // Player movement enable timer
        if (shouldEnablePlayerMovement)
        {
            if (playerMovementEnableTimer >= PlayerMovementEnableTimeOffset)
            {
                shouldEnablePlayerMovement = false;
                EnablePlayerMovement();
            }
            playerMovementEnableTimer += Time.deltaTime;
        }
    }

    private void SetComputerViewActive(bool active)
    {
        isInComputerView = active;
        GameManager.IsInScreenView = active;

        // Disable or enable player movement.
        PlayerScriptsController.SetPlayerMovementEnabled(!active);

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        ToggleComputerTrigger();

        // Probably a temporary solution
        outline.enabled = !active;

        ComputerViewEnabled?.Invoke(active);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (active)
        {
            GameInput.PlayerInputActions.PlayerWalking.Disable();
            GameInput.PlayerInputActions.Computer.Enable();
            CameraController.SetActiveCinemachineCamera(computerCinemachineCamera);
            computerViewEnabledGameEvent.RaiseEvent(this);
        }
        else
        {
            GameInput.PlayerInputActions.Computer.Disable();
            CameraController.SetActiveCinemachineCamera(CameraController.CinemachineMainCamera);
            computerViewDisabledGameEvent.TryRaiseEvent();

            // Set timer to enable player movement
            playerMovementEnableTimer = 0f;
            shouldEnablePlayerMovement = true;
        }

        desk.PlayDeskSitSound();
    }

    public void ToggleComputerTrigger()
    {
        computerTrigger.gameObject.SetActive(!isInComputerView && isComputerEnabled);
    }

    private void EnablePlayerMovement()
    {
        GameInput.PlayerInputActions.PlayerWalking.Enable();
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
