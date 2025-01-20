using Unity.Cinemachine;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    public InteractionTrigger serverTrigger;

    [SerializeField]
    private CinemachineCamera serverCinemachineCamera;

    private Outline outline;

    private bool isInServerView;

    private bool wasToggledThisFrame;

    private void Awake()
    {
        outline = GetComponent<Outline>();

        serverTrigger.InteractVisual = GetComponent<InteractionVisual>();

        serverTrigger.InteractionTriggered += () => SetServerViewActive(true);

        // Action may be changed if a different key binding is preferred.
        GameInput.OnInteractAction += () =>
        {
            if (isInServerView && !wasToggledThisFrame)
            {
                SetServerViewActive(false);
            }
        };

        serverCinemachineCamera.enabled = false;

        isInServerView = false;
        wasToggledThisFrame = false;
    }

    private void Start()
    {
        DetectionManager.DetectionOccured += () => SetServerTriggerEnabled(false);
        DetectionManager.ServerPowerEnabled += SetServerTriggerEnabled;
    }

    private void LateUpdate()
    {
        if (wasToggledThisFrame)
        {
            wasToggledThisFrame = false;
        }
    }

    private void SetServerViewActive(bool active)
    {
        isInServerView = active;
        wasToggledThisFrame = true;
        GameManager.IsInScreenView = active;

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        PlayerScriptsController.SetPlayerMovementEnabled(!active);

        SetServerTriggerEnabled(!active);

        // Probably a temporary solution
        outline.enabled = !active;

        serverViewEnabledGameEvent.RaiseEvent(active);

        if (active)
        {
            GameInput.PlayerInputActions.Computer.Enable();
            CameraController.SetActiveCinemachineCamera(serverCinemachineCamera);
        }
        else
        {
            GameInput.PlayerInputActions.Computer.Disable();
            CameraController.SetActiveCinemachineCamera(CameraController.CinemachineMainCamera);
        }
    }

    private void SetServerTriggerEnabled(bool enabled)
    {
        serverTrigger.gameObject.SetActive(enabled);
    }
}
