using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHUDControllerUI playerHUDController;


    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHUDController = GetComponentInChildren<PlayerHUDControllerUI>();

        SetPlayerMovementEnabled(true);
        SetCanShowPlayerHUD(true);
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
}
