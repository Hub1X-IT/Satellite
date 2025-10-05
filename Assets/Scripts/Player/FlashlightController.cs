using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraFollowObject;

    [SerializeField]
    private float rotationSpeed = 1f;

    private Light lightSource;

    private bool isFlashlightEnabled;

    private void Awake()
    {
        lightSource = GetComponent<Light>();

        lightSource.enabled = false;
        isFlashlightEnabled = false;

        GameInput.OnFlashlightToggleAction += ToggleFlashlight;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraFollowObject.rotation, Time.deltaTime * rotationSpeed);
    }

    private void ToggleFlashlight()
    {
        isFlashlightEnabled = !isFlashlightEnabled;
        lightSource.enabled = isFlashlightEnabled;
    }
}
