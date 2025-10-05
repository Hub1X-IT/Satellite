using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
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

    private static PlayerMovementController playerMovementControllerStatic;
    private static CameraRotationController playerCameraRotationControllerStatic;

    private static PlayerHudUI playerHudUIStatic;

    private static SmartphoneUI smartphoneControllerStatic;

    private static FlashlightController flashlightControllerStatic;

    private void Awake()
    {
        playerMovementControllerStatic = playerMovementController;
        playerCameraRotationControllerStatic = playerCameraRotationController;
        playerHudUIStatic = playerHudUI;
        smartphoneControllerStatic = smartphoneController;
        flashlightControllerStatic = flashlightController;

        SetPlayerMovementEnabled(true);
        SetCanShowPlayerHUD(true);
        SetCanShowSmartphoneUI(false);
    }

    public static void SetPlayerMovementEnabled(bool enabled)
    {
        playerMovementControllerStatic.enabled = enabled;
        playerCameraRotationControllerStatic.enabled = enabled;
    }

    public static void SetCanShowPlayerHUD(bool canShow)
    {
        playerHudUIStatic.CanShowPlayerHUD = canShow;
        playerHudUIStatic.SetPlayerHUDEnabled(!GameManager.IsGamePaused);
    }

    public static void SetCanShowSmartphoneUI(bool canShow)
    {
        smartphoneControllerStatic.SetCanShowSmartphone(canShow);
    }

    public static void SetFlashlightEnabled(bool enabled)
    {
        flashlightControllerStatic.gameObject.SetActive(enabled);
    }
}
