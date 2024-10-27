using UnityEngine;

public class PlayerScriptsController : MonoBehaviour {

    private static PlayerMovementController playerMovementController;
    private static CameraRotationController playerCameraRotationController;

    private static PlayerHUDController playerHUDController;

    private void Awake() {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();

        playerHUDController = GetComponentInChildren<PlayerHUDController>();

        CanShowPlayerHUD(true);
        EnablePlayerMovement(true);
    }

    public static void EnablePlayerMovement(bool targetState) {
        playerMovementController.enabled = targetState;
        playerCameraRotationController.enabled = targetState;
    }

    public static void CanShowPlayerHUD(bool targetState) {
        playerHUDController.CanShowPlayerHUD(targetState);
    }
}
