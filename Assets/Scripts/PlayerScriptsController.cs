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

    private static PlayerMovementController playerMovementControllerStatic;
    private static CameraRotationController playerCameraRotationControllerStatic;

    private static PlayerHudUI playerHudUIStatic;

    private static SmartphoneUI smartphoneControllerStatic;

    private void Awake()
    {
        playerMovementControllerStatic = playerMovementController;
        playerCameraRotationControllerStatic = playerCameraRotationController;
        playerHudUIStatic = playerHudUI;
        smartphoneControllerStatic = smartphoneController;

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
        playerHudUIStatic.SetCanShowPlayerHUD(canShow);
    }

    public static void SetCanShowSmartphoneUI(bool canShow)
    {
        smartphoneControllerStatic.SetCanShowSmartphone(canShow);
    }
}
