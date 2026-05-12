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
    private Interactable computerTrigger;

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

        computerCinemachineCamera.enabled = false;

        isInComputerView = false;
        CanExitComputerView = true;

        shouldEnablePlayerMovement = false;

        wasChangedToInThisFrame = false;

        isComputerTriggerEnabled = false;
    }

    private void Start()
    {
        computerTrigger.OnInteractionTriggered += () =>
        {
            if (isComputerTriggerEnabled)
            {
                SetComputerViewActive(true);
            }
        };

        ComputersChangingUI.ComputerExitTriggered += () =>
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
                Debug.Log($"{gameObject} change to {targetComputer}");
                ChangeCurrentComputer(targetComputer);
            }
        };

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
        GameManager.Instance.IsInScreenView = active;

        // Disable or enable player movement.
        PlayerScriptsController.Instance.SetPlayerMovementEnabled(!active);

        PlayerScriptsController.Instance.SetCanShowPlayerHUD(!active);
        PlayerScriptsController.Instance.SetFlashlightEnabled(!active);

        ToggleComputerTrigger();

        // May be a temporary solution
        if (computerTrigger.IsInteractable)
        {
            outline.SetOutlineEnabled(!active);
        }

        ComputerViewEnabled?.Invoke(active);

        GameManager.Instance.SetCursorShown(active);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (active)
        {
            GameInput.Instance.CurrentInputActions.PlayerWalking.Disable();
            GameInput.Instance.CurrentInputActions.Computer.Enable();
            GameInput.Instance.EscapeAction += ExitComputerView;
            CameraController.Instance.SetActiveCinemachineCamera(computerCinemachineCamera);
            computerViewEnabledGameEvent.RaiseEvent(this);
        }
        else
        {
            GameInput.Instance.CurrentInputActions.Computer.Disable();
            GameInput.Instance.EscapeAction -= ExitComputerView;
            CameraController.Instance.ChangeToMainCinemachineCamera();
            computerViewDisabledGameEvent.RaiseEvent();

            // Set timer to enable player movement
            playerMovementEnableTimer = 0f;
            shouldEnablePlayerMovement = true;
        }

        desk.PlayDeskSitSound();
    }

    public void ChangeCurrentComputer(Computer newComputer)
    {
        isInComputerView = false;
        GameInput.Instance.EscapeAction -= ExitComputerView;

        ToggleComputerTrigger();

        ComputerViewEnabled?.Invoke(false);

        computerViewDisabledGameEvent.RaiseEvent();

        newComputer.ChangeToThisComputer();
    }

    public void ChangeToThisComputer()
    {
        isInComputerView = true;
        GameInput.Instance.EscapeAction += ExitComputerView;

        ToggleComputerTrigger();

        ComputerViewEnabled?.Invoke(true);

        computerViewEnabledGameEvent.RaiseEvent(this);

        CameraController.Instance.SetActiveCinemachineCamera(computerCinemachineCamera);

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
        GameInput.Instance.CurrentInputActions.PlayerWalking.Enable();
    }

    public void ExitComputerView()
    {
        // Method to be invoked by an event listener
        // Used by signal receiver
        if (isInComputerView)
        {
            SetComputerViewActive(false);
            outline.SetOutlineEnabled(false);
        }
    }

    public void SetComputerEnabled(bool enabled)
    {
        IsComputerEnabled = enabled;
        ToggleComputerTrigger();
    }
}
