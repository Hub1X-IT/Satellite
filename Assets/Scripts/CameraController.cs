using System;
using Unity.Cinemachine;
using UnityEngine;

public static class CameraController
{
    [Serializable]
    public struct InitializationData
    {
        public Camera mainCamera;
        public CinemachineCamera cinemachineMainCamera;
    }

    private static Camera currentCamera;

    private static CinemachineCamera currentCinemachineCamera;

    private static Camera mainCamera;

    private static CinemachineCamera cinemachineMainCamera;

    public static Camera CurrentCamera
    {
        get { return currentCamera; }
        set
        {
            // Change active camera
            currentCamera.gameObject.SetActive(false);
            value.gameObject.SetActive(true);
            currentCamera = value;
        }
    }

    public static CinemachineCamera CurrentCinemachineCamera
    {
        get { return currentCinemachineCamera; }
        set
        {
            // Change active Cinemachine camera
            currentCinemachineCamera.gameObject.SetActive(false);
            value.gameObject.SetActive(true);
            currentCinemachineCamera = value;
        }
    }

    public static CinemachineCamera CinemachineMainCamera => cinemachineMainCamera;

    public static Camera MainCamera => mainCamera;

    public static void InitializeOnAwake(InitializationData data)
    {
        mainCamera = data.mainCamera;
        cinemachineMainCamera = data.cinemachineMainCamera;
        InitializeMainCamera();
    }

    public static void ChangeToMainCamera()
    {
        CurrentCamera = mainCamera;
    }

    public static void ChangeToCinemachineMainCamera()
    {
        CurrentCinemachineCamera = cinemachineMainCamera;
    }

    private static void InitializeMainCamera()
    {
        currentCamera = MainCamera;
        currentCamera.gameObject.SetActive(true);

        currentCinemachineCamera = CinemachineMainCamera;
        currentCinemachineCamera.gameObject.SetActive(true);
    }
}