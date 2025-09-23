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

    [SerializeField]
    private Computer computerOnLeft;
    [SerializeField]
    private Computer computerOnRight;

    public Computer ComputerOnLeft => computerOnLeft;
    public Computer ComputerOnRight => computerOnRight;

    [SerializeField]
    private GameEventComputerSO changeToComputerGameEvent;

    private Outline outline;

    private bool isInComputerView;

    private const float PlayerMovementEnableTimeOffset = 0.6f;

    private bool shouldEnablePlayerMovement;

    private float playerMovementEnableTimer;

    public bool CanExitComputerView { get; set; }

    public bool IsComputerEnabled { get; private set; }

    private bool wasChangedToInThisFrame;

    private bool isComputerTriggerEnabled;

    [SerializeField]
    private string computerEnabledInteractMessage;
    [SerializeField]
    private string computerDisabledInteractMessage;

    [SerializeField]
    private string changeToComputerText;
    public string ChangeToComputerText => changeToComputerText;

    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        computerTrigger.InteractVisual = GetComponent<InteractionVisual>();

        computerTrigger.InteractionTriggered += () =>
        {
            if (isComputerTriggerEnabled)
            {
                SetComputerViewActive(true);
            }
        };

        GameInput.OnComputerExitAction += () =>
        {
            if (isInComputerView && CanExitComputerView)
            {
                SetComputerViewActive(false);
            }
        };

        changeToComputerGameEvent.EventRaised += (targetComputer) =>
        {
            if (isInComputerView && CanExitComputerView && !wasChangedToInThisFrame && targetComputer != null)
            {
                Debug.Log(gameObject + " change to " + targetComputer);
                ChangeCurrentComputer(targetComputer);
            }
        };

        computerCinemachineCamera.enabled = false;

        isInComputerView = false;
        CanExitComputerView = true;

        shouldEnablePlayerMovement = false;

        wasChangedToInThisFrame = false;

        isComputerTriggerEnabled = false;

        SetComputerEnabled(true);
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

    private void LateUpdate()
    {
        if (wasChangedToInThisFrame)
        {
            wasChangedToInThisFrame = false;
        }
    }

    private void OnDestroy()
    {
        computerViewEnabledGameEvent.ResetGameEvent();
        computerViewDisabledGameEvent.ResetGameEvent();
        changeToComputerGameEvent.ResetGameEvent();
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

        GameManager.SetCursorShown(active);

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

    public void ChangeCurrentComputer(Computer newComputer)
    {
        isInComputerView = false;

        ToggleComputerTrigger();

        ComputerViewEnabled?.Invoke(false);

        computerViewDisabledGameEvent.TryRaiseEvent();

        newComputer.ChangeToThisComputer();
    }

    public void ChangeToThisComputer()
    {
        isInComputerView = true;

        ToggleComputerTrigger();

        ComputerViewEnabled?.Invoke(true);

        computerViewEnabledGameEvent.RaiseEvent(this);

        CameraController.SetActiveCinemachineCamera(computerCinemachineCamera);

        wasChangedToInThisFrame = true;
    }

    public void ToggleComputerTrigger()
    {
        // computerTrigger.gameObject.SetActive(!isInComputerView && IsComputerEnabled);
        isComputerTriggerEnabled = !isInComputerView && IsComputerEnabled;
        computerTrigger.gameObject.SetActive(!isInComputerView);
        computerTrigger.InteractVisual.SetInteractMessage(isComputerTriggerEnabled ? computerEnabledInteractMessage : computerDisabledInteractMessage);
    }

    private void EnablePlayerMovement()
    {
        GameInput.PlayerInputActions.PlayerWalking.Enable();
    }

    public void ExitComputerView()
    {
        // Method to be invoked by an event listener
        // Used by signal receiver
        if (isInComputerView)
        {
            SetComputerViewActive(false);
            outline.enabled = false;
        }
    }

    public void SetComputerEnabled(bool enabled)
    {
        IsComputerEnabled = enabled;
        ToggleComputerTrigger();
    }
}
