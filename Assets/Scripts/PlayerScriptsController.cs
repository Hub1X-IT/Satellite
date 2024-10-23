using UnityEngine;

public class PlayerScriptsController : MonoBehaviour {

    private PlayerMovementController playerMovementController;
    private CameraRotationController playerCameraRotationController;

    private void Awake() {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCameraRotationController = GetComponent<CameraRotationController>();
    }

    public void EnablePlayerMovement(bool targetState) {
        playerMovementController.enabled = targetState;
        playerCameraRotationController.enabled = targetState;
    }
}
