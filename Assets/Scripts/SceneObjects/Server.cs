using Unity.Cinemachine;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    public InteractionTrigger serverTrigger;

    [SerializeField]
    private CinemachineCamera serverCinemachineCamera;

    [SerializeField]
    private MeshRenderer serverMeshRenderer;
    [SerializeField]
    private Material serrverOnMaterial;
    [SerializeField]
    private Material serverOffMaterial;

    private Outline outline;

    private bool isInServerView;

    private bool wasToggledThisFrame;

    [SerializeField]
    private string serverEnabledInteractMessage;
    [SerializeField]
    private string serverDisabledInteractMessage;

    private bool isServerEnabled;

    private void Awake()
    {
        outline = GetComponentInChildren<Outline>();

        serverTrigger.InteractVisual = GetComponent<InteractionVisual>();

        serverTrigger.InteractionTriggered += () =>
        {
            if (isServerEnabled)
            {
                SetServerViewActive(true);
            }
        };

        // Action may be changed if a different key binding is preferred.
        GameInput.OnComputerExitAction += () =>
        {
            if (isInServerView && !wasToggledThisFrame)
            {
                SetServerViewActive(false);
            }
        };

        serverCinemachineCamera.enabled = false;

        isInServerView = false;
        wasToggledThisFrame = false;
        SetServerEnabled(true);
        SetServerTriggerEnabled(true);
    }

    private void Start()
    {
        DetectionManager.DetectionOccured += () =>
        {
            SetServerEnabled(false);
            SetServerOnOffMaterial(false);
        };

        DetectionManager.ServerPowerEnabled += (enabled) =>
        {
            SetServerEnabled(enabled);
            SetServerOnOffMaterial(enabled);
        };
    }

    private void LateUpdate()
    {
        if (wasToggledThisFrame)
        {
            wasToggledThisFrame = false;
        }
    }

    private void OnDestroy()
    {
        serverViewEnabledGameEvent.ResetGameEvent();
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

        GameManager.SetCursorShown(active);

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
    private void SetServerEnabled(bool enabled)
    {
        isServerEnabled = enabled;
        serverTrigger.InteractVisual.SetInteractMessage(enabled ? serverEnabledInteractMessage : serverDisabledInteractMessage);
    }
    private void SetServerOnOffMaterial(bool enabled)
    {
        serverMeshRenderer.material = enabled ? serrverOnMaterial : serverOffMaterial;
    }
}
