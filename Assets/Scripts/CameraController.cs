using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance { get; private set; }
    
    private Camera currentCamera;
    [SerializeField] private Camera mainCamera;
    
    private CinemachineCamera currentCinemachineCamera;
    [SerializeField] private CinemachineCamera cinemachineMainCamera;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        InitializeMainCamera();
    }


    public void ChangeCamera(Camera newCamera) {
        currentCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        currentCamera = newCamera;
    }

    public void ChangeCinemachineCamera(CinemachineCamera newCinemachineCamera) {
        currentCinemachineCamera.gameObject.SetActive(false);
        newCinemachineCamera.gameObject.SetActive(true);
        currentCinemachineCamera = newCinemachineCamera;
    }

    public void ChangeToMainCamera() {
        currentCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        currentCamera = mainCamera;
    }

    public void ChangeToCinemachineMainCamera() {
        currentCinemachineCamera.gameObject.SetActive(false);
        cinemachineMainCamera.gameObject.SetActive(true);
        currentCinemachineCamera = cinemachineMainCamera;
    }

    private void InitializeMainCamera() {
        currentCamera = mainCamera;
        currentCamera.gameObject.SetActive(true);

        currentCinemachineCamera = cinemachineMainCamera;
        currentCinemachineCamera.gameObject.SetActive(true);
    }

    public Camera GetCurrentCamera() { return currentCamera; }

    public CinemachineCamera GetCurrentCinemachineCamera() { return currentCinemachineCamera; }
}