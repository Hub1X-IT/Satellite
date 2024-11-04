using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHUDControllerUI playerHUDController;

    private static SmartphoneControllerUI smartphoneController;


    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHUDController = GetComponentInChildren<PlayerHUDControllerUI>();

        smartphoneController = GetComponentInChildren<SmartphoneControllerUI>(true);

        SetPlayerMovementEnabled(true);
        SetCanShowPlayerHUD(true);
        SetCanShowSmartphoneUI(false);
    }

    public static void SetPlayerMovementEnabled(bool enabled)
    {
        playerMovementController.enabled = enabled;
        playerCameraRotationController.enabled = enabled;
    }

    public static void SetCanShowPlayerHUD(bool canShow)
    {
        playerHUDController.SetCanShowPlayerHUD(canShow);
    }

    public static void SetCanShowSmartphoneUI(bool canShow)
    {
        smartphoneController.SetCanShowSmartphone(canShow);
    }
}
