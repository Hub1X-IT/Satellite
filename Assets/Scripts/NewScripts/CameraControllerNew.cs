using Unity.Cinemachine;
using UnityEngine;

public class CameraControllerNew {


    private static Camera currentCamera;
    public static Camera MainCamera { get; private set; }

    private static CinemachineCamera currentCinemachineCamera;
    public static CinemachineCamera CinemachineMainCamera { get; private set; }

    public static void InitializeOnAwake(Camera mainCamera, CinemachineCamera cinemachineMainCamera) {
        MainCamera = mainCamera;
        CinemachineMainCamera = cinemachineMainCamera;
    }

    public static void InitializeOnStart() {
        InitializeMainCamera();
    }

    public static void ChangeCamera(Camera newCamera) {
        currentCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        currentCamera = newCamera;
    }

    public static void ChangeCinemachineCamera(CinemachineCamera newCinemachineCamera) {
        currentCinemachineCamera.gameObject.SetActive(false);
        newCinemachineCamera.gameObject.SetActive(true);
        currentCinemachineCamera = newCinemachineCamera;
    }

    public void ChangeToMainCamera() {
        currentCamera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        currentCamera = MainCamera;
    }

    public void ChangeToCinemachineMainCamera() {
        currentCinemachineCamera.gameObject.SetActive(false);
        CinemachineMainCamera.gameObject.SetActive(true);
        currentCinemachineCamera = CinemachineMainCamera;
    }

    private static void InitializeMainCamera() {
        currentCamera = MainCamera;
        currentCamera.gameObject.SetActive(true);

        currentCinemachineCamera = CinemachineMainCamera;
        currentCinemachineCamera.gameObject.SetActive(true);
    }

    public Camera GetCurrentCamera() { return currentCamera; }

    public CinemachineCamera GetCurrentCinemachineCamera() { return currentCinemachineCamera; }

}