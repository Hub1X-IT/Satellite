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
    private SmartphoneController smartphoneController;
    [SerializeField]
    private FlashlightController flashlightController;
    [SerializeField]
    private InteractionController playerInteractionController;

    private bool canShowHUD = true;
    private bool canShowFPHUD = true;

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
        GameManager.Instance.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);
        SetPlayerHUDEnabled(!GameManager.Instance.IsGamePaused);
        playerHudUI.SetPlayerObjectivesHUDEnabled(!GameManager.Instance.IsGamePaused);
        SetPlayerMovementEnabled(true);
        SetCanShowPlayerHUD(true);
        SetCanShowSmartphone(false);
    }

    public void SetPlayerMovementEnabled(bool enabled)
    {
        playerMovementController.StopMovement();
        playerMovementController.enabled = enabled;
        playerCameraRotationController.enabled = enabled;
    }

    public void SetCanShowPlayerHUD(bool canShow)
    {
        canShowHUD = canShow;
        SetPlayerHUDEnabled(!GameManager.Instance.IsGamePaused);
    }

    public void SetPlayerHUDEnabled(bool enabled)
    {
        SetPlayerFPHUDEnabled(enabled && canShowHUD);
        playerHudUI.SetPlayerHUDEnabled(enabled && canShowHUD);
    }

    public void SetCanShowPlayerFPHUD(bool canShow)
    {
        canShowFPHUD = canShow;
        SetCanShowSmartphone(canShow);
        SetPlayerFPHUDEnabled(!GameManager.Instance.IsGamePaused);
    }

    public void SetPlayerFPHUDEnabled(bool enabled)
    {
        playerHudUI.SetPlayerFPHUDEnabled(enabled && canShowFPHUD);
    }

    public void SetCrosshairEnabled(bool enabled) => playerHudUI.SetCrosshairEnabled(enabled);

    public void SetFlashlightInfoEnabled(bool enabled) => playerHudUI.SetFlashlightInfoEnabled(enabled);

    public void SetCanShowSmartphone(bool canShow) => smartphoneController.SetCanShowSmartphone(canShow);

    public void UpdateFlashlightState() => flashlightController.UpdateFlashlightState();

    public void SetInteractionEnabled(bool enabled) => playerInteractionController.SetInteractionEnabled(enabled);
}
