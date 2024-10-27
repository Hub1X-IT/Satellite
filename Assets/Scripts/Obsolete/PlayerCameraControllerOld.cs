using UnityEngine;

public class PlayerCameraControllerOld : MonoBehaviour {

    [SerializeField] private Transform cameraFollowObject;


    private Vector2 rotationInput;


    private void Update() {
        HandleRotation();
    }


    private void HandleRotation() {
        rotationInput = GameInput.GetRotationVector();

        // Handle Y axis rotation - rotating the player
        Vector3 playerRotation = new Vector3(0f, rotationInput.x, 0f);
        transform.Rotate(playerRotation * GameSettingsManager.MouseSensitivity * Time.deltaTime);

        // Handle X axis rotation - rotating only the camera
        Vector3 cameraRotation = cameraFollowObject.rotation.eulerAngles;

        if (cameraRotation.x > 180f) cameraRotation.x -= 360f;

        /*
        eulerAngles always maps rotation to a positive value so if it is more than 180 degrees, you have to make it negative so that you can clamp it
        For example: when rotation equals -20, you get 340 which is greater than 90 so it would be clamped to 90 degrees, although it should have been left as it is
        */

        cameraRotation.x += -rotationInput.y * GameSettingsManager.MouseSensitivity * Time.deltaTime;

        // Clamp camera X rotation
        float minXRotation = -90f;
        float maxXRotation = 90f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, minXRotation, maxXRotation);

        cameraFollowObject.rotation = Quaternion.Euler(cameraRotation);
    }
}
