using Unity.Cinemachine;
using UnityEngine;

public class CameraControllerOld : MonoBehaviour
{
    public static CameraControllerOld Instance { get; private set; }

    private static Camera currentCamera;
    public Camera mainCamera;

    private static CinemachineCamera currentCinemachineCamera;
    public CinemachineCamera cinemachineMainCamera;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeMainCamera();
    }


    public static void ChangeCamera(Camera newCamera)
    {
        currentCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        currentCamera = newCamera;
    }

    public static void ChangeCinemachineCamera(CinemachineCamera newCinemachineCamera)
    {
        currentCinemachineCamera.gameObject.SetActive(false);
        newCinemachineCamera.gameObject.SetActive(true);
        currentCinemachineCamera = newCinemachineCamera;
    }

    public void ChangeToMainCamera()
    {
        currentCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        currentCamera = mainCamera;
    }

    public void ChangeToCinemachineMainCamera()
    {
        currentCinemachineCamera.gameObject.SetActive(false);
        cinemachineMainCamera.gameObject.SetActive(true);
        currentCinemachineCamera = cinemachineMainCamera;
    }

    private void InitializeMainCamera()
    {
        currentCamera = mainCamera;
        currentCamera.gameObject.SetActive(true);

        currentCinemachineCamera = cinemachineMainCamera;
        currentCinemachineCamera.gameObject.SetActive(true);
    }

    public Camera GetCurrentCamera() { return currentCamera; }

    public CinemachineCamera GetCurrentCinemachineCamera() { return currentCinemachineCamera; }
}