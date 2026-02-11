using Unity.Cinemachine;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    public Interactable serverTrigger;

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

        serverTrigger.OnInteractionTriggered += () =>
        {
            if (isServerEnabled)
            {
                SetServerViewActive(true);
            }
        };

        // Action may be changed if a different key binding is preferred.
        ComputersChangingUI.ComputerExitTriggered += () =>
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
        PowerManager.Instance.OnPowerStateChanged += (isPowerOn) =>
        {
            SetServerEnabled(isPowerOn);
            SetServerOnOffMaterial(isPowerOn);
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
        GameManager.Instance.IsInScreenView = active;

        PlayerScriptsController.Instance.SetCanShowPlayerHUD(!active);
        PlayerScriptsController.Instance.SetFlashlightEnabled(!active);

        PlayerScriptsController.Instance.SetPlayerMovementEnabled(!active);

        SetServerTriggerEnabled(!active);

        // Probably a temporary solution
        outline.SetOutlineEnabled(!active);

        serverViewEnabledGameEvent.RaiseEvent(active);

        GameManager.Instance.SetCursorShown(active);

        if (active)
        {
            GameInput.Instance.CurrentInputActions.Computer.Enable();
            CameraController.Instance.SetActiveCinemachineCamera(serverCinemachineCamera);
        }
        else
        {
            GameInput.Instance.CurrentInputActions.Computer.Disable();
            CameraController.Instance.ChangeToMainCinemachineCamera();
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
