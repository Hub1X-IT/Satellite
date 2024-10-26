using UnityEngine;

public class PlayerScriptsControllerOld : MonoBehaviour {
    
    public static PlayerScriptsControllerOld Instance { get; private set; }

    [SerializeField] private PlayerMovementController playerMovementController;

    [SerializeField] private CameraRotationController playerCameraRotationController;

    private void Awake() {
        Instance = this;
    }

    public void EnablePlayerMovementController(bool targetState) { playerMovementController.enabled = targetState; }

    public void EnablePlayerCameraRotationController(bool targetState) { playerCameraRotationController.enabled = targetState; }


}
