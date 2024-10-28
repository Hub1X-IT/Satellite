using UnityEngine;

public class PlayerScriptsController : MonoBehaviour
{
    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHUDController playerHUDController;

    private static bool isPlayerMovementEnabled;

    private static bool canShowPlayerHUD;


    public static bool IsPlayerMovementEnabled
    {
        get => isPlayerMovementEnabled;
        set
        {
            // Enable/disable player movement.
            isPlayerMovementEnabled = value;
            playerMovementController.enabled = value;
            playerCameraRotationController.enabled = value;
        }
    }

    public static bool CanShowPlayerHUD
    {
        get => canShowPlayerHUD;
        set
        {
            canShowPlayerHUD = value;
            playerHUDController.CanShowPlayerHUD = value;
        }
    }


    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHUDController = GetComponentInChildren<PlayerHUDController>();

        IsPlayerMovementEnabled = true;
        CanShowPlayerHUD = true;
    }
}
