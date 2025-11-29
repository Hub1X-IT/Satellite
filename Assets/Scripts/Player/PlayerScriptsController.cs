using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    public static PlayerScriptsController Instance { get; private set; }

    [SerializeField]
    private PlayerMovementController playerMovementController;
    [SerializeField]
    private CameraRotationController playerCameraRotationController;
    [SerializeField]
    private PlayerHudUI playerHudUI;
    [SerializeField]
    private SmartphoneUI smartphoneController;
    [SerializeField]
    private FlashlightController flashlightController;
    [SerializeField]
    private InteractionController playerInteractionController;

    public bool IsInteractionEnabled => playerInteractionController.IsInteractionEnabled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple Instances of {nameof(PlayerScriptsController)} detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetPlayerMovementEnabled(true);
        SetCanShowPlayerHUD(true);
        SetCanShowSmartphoneUI(false);
    }

    public void SetPlayerMovementEnabled(bool enabled)
    {
        playerMovementController.enabled = enabled;
        playerCameraRotationController.enabled = enabled;
    }

    public void SetCanShowPlayerHUD(bool canShow)
    {
        playerHudUI.CanShowPlayerHUD = canShow;
        playerHudUI.SetPlayerHUDEnabled(!GameManager.Instance.IsGamePaused);
    }

    public void SetCanShowSmartphoneUI(bool canShow)
    {
        smartphoneController.SetCanShowSmartphone(canShow);
    }

    public void SetFlashlightEnabled(bool enabled)
    {
        flashlightController.gameObject.SetActive(enabled);
    }

    public void SetInteractionEnabled(bool enabled)
    {
        playerInteractionController.SetInteractionEnabled(enabled);
    }
}
