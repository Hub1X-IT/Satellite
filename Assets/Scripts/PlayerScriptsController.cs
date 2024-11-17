using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHudUI playerHudUI;

    private static SmartphoneControllerUI smartphoneController;


    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHudUI = GetComponentInChildren<PlayerHudUI>();

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
        playerHudUI.SetCanShowPlayerHUD(canShow);
    }

    public static void SetCanShowSmartphoneUI(bool canShow)
    {
        smartphoneController.SetCanShowSmartphone(canShow);
    }
}
