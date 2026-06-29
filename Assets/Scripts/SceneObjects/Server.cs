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
    [SerializeField]
    private string interactionDisabledInteractMessage;

    private bool isServerEnabled;
    private bool isInteractionEnabled;

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

        serverTrigger.OnSetInteractable += (interactable) =>
        {
            isInteractionEnabled = interactable;
            UpdateInteractMessage();
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
        // Uncomment if necessary
        // if (isInServerView == active) return;
        isInServerView = active;
        wasToggledThisFrame = true;
        GameManager.Instance.IsInScreenView = active;

        PlayerScriptsController.Instance.SetCanShowPlayerFPHUD(!active);
        PlayerScriptsController.Instance.UpdateFlashlightState();

        PlayerScriptsController.Instance.SetPlayerMovementEnabled(!active);

        SetServerTriggerEnabled(!active);

        // Probably a temporary solution
        outline.SetOutlineEnabled(!active);

        serverViewEnabledGameEvent.RaiseEvent(active);
        GameManager.Instance.SetCursorShown(active);

        if (active)
        {
            GameInput.Instance.CurrentInputActions.PlayerWalking.Disable();
            GameInput.Instance.CurrentInputActions.Computer.Enable();
            GameInput.Instance.EscapeAction += ExitServerView;
            CameraController.Instance.SetActiveCinemachineCamera(serverCinemachineCamera);
        }
        else
        {
            GameInput.Instance.CurrentInputActions.Computer.Disable();
            GameInput.Instance.CurrentInputActions.PlayerWalking.Enable();
            GameInput.Instance.EscapeAction -= ExitServerView;
            CameraController.Instance.ChangeToMainCinemachineCamera();
        }
    }

    private void ExitServerView() => SetServerViewActive(false);

    private void SetServerTriggerEnabled(bool enabled)
    {
        serverTrigger.gameObject.SetActive(enabled);
    }
    private void SetServerEnabled(bool enabled)
    {
        isServerEnabled = enabled;
        UpdateInteractMessage();
    }
    private void SetServerOnOffMaterial(bool enabled)
    {
        serverMeshRenderer.material = enabled ? serrverOnMaterial : serverOffMaterial;
    }

    private void UpdateInteractMessage()
    {
        if (isInteractionEnabled)
        {
            if (isServerEnabled)
            {
                serverTrigger.InteractVisual.SetInteractMessage(serverEnabledInteractMessage);
                serverTrigger.InteractVisual.ShouldShowInteractionIcon = true;
            }
            else
            {
                serverTrigger.InteractVisual.SetInteractMessage(serverDisabledInteractMessage);
                serverTrigger.InteractVisual.ShouldShowInteractionIcon = false;
            }
        }
        else
        {
            serverTrigger.InteractVisual.SetInteractMessage(interactionDisabledInteractMessage);
            serverTrigger.InteractVisual.ShouldShowInteractionIcon = false;
        }
    }
}
