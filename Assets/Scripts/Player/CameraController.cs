using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private CinemachineCamera cinemachineMainCamera;

    private Camera activeCamera;
    private CinemachineCamera activeCinemachineCamera;

    public Camera MainCamera => mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(CameraController)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        activeCamera = mainCamera;
        activeCinemachineCamera = cinemachineMainCamera;
    }

    private void Start()
    {
        ChangeToMainCamera();
        ChangeToMainCinemachineCamera();
    }

    public void SetActiveCamera(Camera camera)
    {
        if (activeCamera != null)
        {
            activeCamera.enabled = false;
        }
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    public void SetActiveCinemachineCamera(CinemachineCamera cinemachineCamera)
    {
        if (activeCinemachineCamera != null)
        {
            activeCinemachineCamera.enabled = false;
        }
        activeCinemachineCamera = cinemachineCamera;
        activeCinemachineCamera.enabled = true;
    }

    public void ChangeToMainCamera()
    {
        SetActiveCamera(mainCamera);
    }

    public void ChangeToMainCinemachineCamera()
    {
        SetActiveCinemachineCamera(cinemachineMainCamera);
    }
}