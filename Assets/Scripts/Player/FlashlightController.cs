using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraFollowObject;

    [SerializeField]
    private float rotationSpeed = 10f;

    private Light lightSource;

    private bool isFlashlightEnabledByPlayer;

    private void Awake()
    {
        lightSource = GetComponent<Light>();

        isFlashlightEnabledByPlayer = false;
    }

    private void Start()
    {
        GameInput.Instance.OnFlashlightToggleAction += () =>
        {
            isFlashlightEnabledByPlayer = !isFlashlightEnabledByPlayer;
            UpdateFlashlightState();
        };

        UpdateFlashlightState();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraFollowObject.rotation, Time.deltaTime * rotationSpeed);
    }

    public void UpdateFlashlightState()
    {
        lightSource.enabled = isFlashlightEnabledByPlayer && !GameManager.Instance.IsInSmartphoneView && !GameManager.Instance.IsInScreenView;
        PlayerScriptsController.Instance.SetFlashlightInfoEnabled(!GameManager.Instance.IsInSmartphoneView);
    }
}