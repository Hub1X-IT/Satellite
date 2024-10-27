using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHUDController playerHUDController;

    private static bool isPlayerMovementEnabled;
    public static bool IsPlayerMovementEnabled
    {
        get => isPlayerMovementEnabled;
        set
        {
            // Enable/disable player movement.
            playerMovementController.enabled = value;
            playerCameraRotationController.enabled = value;

            isPlayerMovementEnabled = value;
        }
    }

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHUDController = GetComponentInChildren<PlayerHUDController>();

        CanShowPlayerHUD(true);
        IsPlayerMovementEnabled = true;
    }

    public static void CanShowPlayerHUD(bool state)
    {
        playerHUDController.CanShowPlayerHUD = state;
    }
}
