using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public static CameraController Instance { get; private set; }


    [SerializeField] private SerializableDictionary<Cameras, CinemachineCamera> cameraSerializableDictionary;
    private Dictionary<Cameras, CinemachineCamera> cameraDictionary;
    

    public enum Cameras {
        MainCamera,
        DeskCamera,
        MonitorCamera,
        LaptopCamera,
    }


    private Cameras previousCamera;
    private Cameras currentCamera;


    private void Awake() {
        Instance = this;
        cameraDictionary = cameraSerializableDictionary.GetDictionary();
    }

    private void Start() {
        InitializeMainCamera();
    }


    private void UpdateCamera() {
        TurnCameraOff(previousCamera);
        TurnCameraOn(currentCamera);
    }


    public void SetActiveCamera(Cameras camera) {
        previousCamera = currentCamera;
        currentCamera = camera;
        UpdateCamera();
    }

    public Cameras GetActiveCamera() {
        return currentCamera;
    }


    private void TurnCameraOn(Cameras camera) {
        cameraDictionary[camera]?.gameObject.SetActive(true);
    }

    private void TurnCameraOff(Cameras camera) {
        cameraDictionary[camera]?.gameObject.SetActive(false);
    }


    private void InitializeMainCamera() {
        foreach (Cameras camera in cameraDictionary.Keys) {
            TurnCameraOff(camera);
        }
        previousCamera = Cameras.MainCamera;
        currentCamera = Cameras.MainCamera;
        TurnCameraOn(currentCamera);        
    }
}