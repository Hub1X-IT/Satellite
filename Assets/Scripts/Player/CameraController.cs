using System;
using Unity.Cinemachine;
using UnityEngine;

public static class CameraController
{
    [Serializable]
    public struct InitializationData
    {
        public Camera MainCamera;
        public CinemachineCamera CinemachineMainCamera;
    }

    public static Camera ActiveCamera { get; private set; }

    public static CinemachineCamera ActiveCinemachineCamera { get; private set; }


    public static Camera MainCamera { get; private set; }

    public static CinemachineCamera CinemachineMainCamera { get; private set; }


    public static void OnAwake(InitializationData data)
    {
        MainCamera = data.MainCamera;
        CinemachineMainCamera = data.CinemachineMainCamera;

        ActiveCamera = MainCamera;
        SetActiveCamera(MainCamera);

        ActiveCinemachineCamera = CinemachineMainCamera;
        SetActiveCinemachineCamera(CinemachineMainCamera);
    }

    public static void SetActiveCamera(Camera camera)
    {
        /*
        ActiveCamera.gameObject.SetActive(false);
        ActiveCamera = camera;
        ActiveCamera.gameObject.SetActive(true);
        */
        if (ActiveCamera != null)
        {
            ActiveCamera.enabled = false;
        }
        ActiveCamera = camera;
        ActiveCamera.enabled = true;
    }

    public static void SetActiveCinemachineCamera(CinemachineCamera cinemachineCamera)
    {
        /*
        ActiveCinemachineCamera.gameObject.SetActive(false);
        ActiveCinemachineCamera = cinemachineCamera;
        ActiveCinemachineCamera.gameObject.SetActive(true);
        */
        if (ActiveCinemachineCamera != null)
        {
            ActiveCinemachineCamera.enabled = false;
        }
        ActiveCinemachineCamera = cinemachineCamera;
        ActiveCinemachineCamera.enabled = true;
    }

    public static void SetCameraRenderTexture(Camera camera, RenderTexture renderTexture)
    {
        camera.targetTexture = renderTexture;
    }
}