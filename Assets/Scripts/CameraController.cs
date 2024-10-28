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

    private static Camera activeCamera;

    private static CinemachineCamera activeCinemachineCamera;

    private static Camera mainCamera;

    private static CinemachineCamera cinemachineMainCamera;

    public static Camera ActiveCamera
    {
        get { return activeCamera; }
        set
        {
            // Change active camera
            activeCamera.gameObject.SetActive(false);
            value.gameObject.SetActive(true);
            activeCamera = value;
        }
    }

    public static CinemachineCamera ActiveCinemachineCamera
    {
        get { return activeCinemachineCamera; }
        set
        {
            // Change active Cinemachine camera
            activeCinemachineCamera.gameObject.SetActive(false);
            value.gameObject.SetActive(true);
            activeCinemachineCamera = value;
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
        ActiveCamera = mainCamera;
    }

    public static void ChangeToCinemachineMainCamera()
    {
        ActiveCinemachineCamera = cinemachineMainCamera;
    }

    private static void InitializeMainCamera()
    {
        activeCamera = MainCamera;
        activeCamera.gameObject.SetActive(true);

        activeCinemachineCamera = CinemachineMainCamera;
        activeCinemachineCamera.gameObject.SetActive(true);
    }
}