using UnityEngine;

public class CameraRotationController : MonoBehaviour {


    [SerializeField] private Transform xAxisRotationObject;
    [SerializeField] private Transform yAxisRotationObject;


    [SerializeField] private bool clampXRotation = false;
    [SerializeField] private float minXRotation;
    [SerializeField] private float maxXRotation;

    [SerializeField] private bool clampYRotation = false;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;


    private void Update() {
        HandleRotation();
    }


    private void HandleRotation() {
        Vector2 rotationInput = GameInput.RotationVector;


        Vector3 xAxisRotationObjectRotation = xAxisRotationObject.rotation.eulerAngles;

        if (xAxisRotationObjectRotation.x > 180f) xAxisRotationObjectRotation.x -= 360f;
        xAxisRotationObjectRotation.x += -rotationInput.y * GameSettingsManager.MouseSensitivity * Time.deltaTime;
        if (clampXRotation) xAxisRotationObjectRotation.x = Mathf.Clamp(xAxisRotationObjectRotation.x, minXRotation, maxXRotation);

        xAxisRotationObject.rotation = Quaternion.Euler(xAxisRotationObjectRotation);


        Vector3 yAxisRotationObjectRotation = yAxisRotationObject.rotation.eulerAngles;

        if (yAxisRotationObjectRotation.y > 180f) yAxisRotationObjectRotation.y -= 360f;
        yAxisRotationObjectRotation.y += rotationInput.x * GameSettingsManager.MouseSensitivity * Time.deltaTime;
        if (clampYRotation) yAxisRotationObjectRotation.y = Mathf.Clamp(yAxisRotationObjectRotation.y, minYRotation, maxYRotation);

        yAxisRotationObject.rotation = Quaternion.Euler(yAxisRotationObjectRotation);
    }

    public void SetLocalRotation(float xRotation, float yRotation) {
        Vector3 xAxisRotationObjectRotation = new Vector3(xRotation, xAxisRotationObject.localRotation.y, xAxisRotationObject.localRotation.z);
        xAxisRotationObject.localRotation = Quaternion.Euler(xAxisRotationObjectRotation);

        Vector3 yAxisRotationObjectRotation = new Vector3(yAxisRotationObject.localRotation.x, yRotation, yAxisRotationObject.localRotation.z);
        yAxisRotationObject.localRotation = Quaternion.Euler(yAxisRotationObjectRotation);
    }
}