using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public event Action<bool> DeskViewEnabled;

    [SerializeField]
    private InteractionTrigger deskTrigger;

    [SerializeField]
    private CinemachineCamera deskCinemachineCamera;

    [SerializeField]
    private AudioSource deskSitAudioSource;

    [SerializeField]
    private Computer[] childComputers;

    private CameraRotationController deskCameraRotationController;

    private const float PlayerMovementEnableTimeOffset = 0.6f;

    private bool shouldEnablePlayerMovement;

    private float playerMovementEnableTimer;

    public bool CanExitDeskView { get; set; }

    public CinemachineCamera DeskCinemachineCamera => deskCinemachineCamera;

    private void Awake()
    {
        deskTrigger.InteractVisual = GetComponent<InteractionVisual>();
        deskCameraRotationController = GetComponent<CameraRotationController>();

        GameInput.OnExitDeskViewAction += () =>
        {
            if (CanExitDeskView)
            {
                SetDeskViewActive(false);
            }
        };

        deskTrigger.InteractionTriggered += () => SetDeskViewActive(true);

        deskCinemachineCamera.enabled = false;

        SetDeskCameraRotationEnabled(false);
        CanExitDeskView = true;
        shouldEnablePlayerMovement = false;

        SetAllComputersEnabled(false);
    }

    private void Start()
    {
        ServerConnectionManager.ServerConnectionEnabled += SetAllComputersEnabled;
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

    private void OnDestroy()
    {
        DeskViewEnabled = null;
    }

    private void SetDeskViewActive(bool active)
    {
        // Disable or enable desk trigger.
        deskTrigger.gameObject.SetActive(!active);

        // Disable or enable player movement.
        PlayerScriptsController.SetPlayerMovementEnabled(!active);

        // Enable or disable desk camera control.
        SetDeskCameraRotationEnabled(active);

        // Invoke enter/exit event.
        DeskViewEnabled?.Invoke(active);

        CameraController.SetActiveCinemachineCamera(active ? DeskCinemachineCamera : CameraController.CinemachineMainCamera);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (active)
        {
            GameInput.PlayerInputActions.PlayerWalking.Disable();
            GameInput.PlayerInputActions.Desk.Enable();
        }
        else
        {
            GameInput.PlayerInputActions.Desk.Disable();
            // Set timer to enable player movement
            playerMovementEnableTimer = 0f;
            shouldEnablePlayerMovement = true;
        }

        deskSitAudioSource.Play();
    }

    private void EnablePlayerMovement()
    {
        GameInput.PlayerInputActions.PlayerWalking.Enable();
    }

    public void SetDeskCameraRotationEnabled(bool enabled)
    {
        deskCameraRotationController.enabled = enabled;
    }

    public void SetAllComputersEnabled(bool enabled)
    {
        foreach (var computer in childComputers)
        {
            computer.IsComputerEnabled = enabled;
            computer.ToggleComputerTrigger();
        }
    }
}
