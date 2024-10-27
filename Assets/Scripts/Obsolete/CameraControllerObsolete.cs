using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraControllerObsolete : MonoBehaviour
{
    public static CameraControllerObsolete Instance { get; private set; }


    [SerializeField] private SerializableDictionary<Cameras, Camera> CameraSerializableDictionary;
    private Dictionary<Cameras, Camera> cameraDictionary;


    [SerializeField] private SerializableDictionary<CinemachineCameras, CinemachineCamera> CinemachineCameraSerializableDictionary;
    private Dictionary<CinemachineCameras, CinemachineCamera> cinemachineCameraDictionary;


    public enum Cameras
    {
        MainCamera,
        MonitorUICamera,
        LaptopUICamera,
    }


    public enum CinemachineCameras
    {
        CinemachineMainCamera,
        CinemachineDeskCamera,
    }

    private Cameras previousCamera;
    private Cameras currentCamera;


    private CinemachineCameras previousCinemachineCamera;
    private CinemachineCameras currentCinemachineCamera;


    private void Awake()
    {
        Instance = this;
        cameraDictionary = CameraSerializableDictionary.Dictionary;
        cinemachineCameraDictionary = CinemachineCameraSerializableDictionary.Dictionary;
    }


    private void Start()
    {
        InitializeMainCamera();
    }

    private void UpdateCamera()
    {
        TurnCameraOn(previousCamera, false);
        TurnCameraOn(currentCamera, true);
    }

    private void UpdateCinemachineCamera()
    {
        TurnCinemachineCameraOn(previousCinemachineCamera, false);
        TurnCinemachineCameraOn(currentCinemachineCamera, true);
    }

    public void SetActiveCamera(Cameras camera)
    {
        previousCamera = currentCamera;
        currentCamera = camera;
        UpdateCamera();
    }

    public void SetActiveCinemachineCamera(CinemachineCameras cinemachineCamera)
    {
        previousCinemachineCamera = currentCinemachineCamera;
        currentCinemachineCamera = cinemachineCamera;
        UpdateCinemachineCamera();
    }

    private void TurnCameraOn(Cameras camera, bool targetState)
    {
        if (cameraDictionary[camera] != null) cameraDictionary[camera].gameObject.SetActive(targetState);
    }

    private void TurnCinemachineCameraOn(CinemachineCameras cinemachineCamera, bool targetState)
    {
        if (cinemachineCameraDictionary[cinemachineCamera] != null) cinemachineCameraDictionary[cinemachineCamera].gameObject.SetActive(targetState);
    }

    private void InitializeMainCamera()
    {
        foreach (Cameras camera in cameraDictionary.Keys)
        {
            TurnCameraOn(camera, false);
        }
        previousCamera = Cameras.MainCamera;
        currentCamera = Cameras.MainCamera;
        TurnCameraOn(currentCamera, true);

        foreach (CinemachineCameras cinemachineCamera in cinemachineCameraDictionary.Keys)
        {
            TurnCinemachineCameraOn(cinemachineCamera, false);
        }
        previousCinemachineCamera = CinemachineCameras.CinemachineMainCamera;
        currentCinemachineCamera = CinemachineCameras.CinemachineMainCamera;
        TurnCinemachineCameraOn(currentCinemachineCamera, true);
    }

    public Cameras GetActiveCamera() { return currentCamera; }

    public CinemachineCameras GetActiveCinemachineCamera() { return currentCinemachineCamera; }
}