using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public event Action<bool> DeskViewEnabled;

    [SerializeField]
    private CinemachineCamera cinemachineDeskCamera;

    private DeskTrigger deskTrigger;

    private CameraRotationController deskCameraRotationController;

    private readonly Vector3 deskCameraDefaultRotation = new(0f, 180f, 0f);


    public bool IsDeskViewActive { get; private set; }
    
    public bool IsDeskCameraRotationEnabled { get; private set; }

    public bool CanExitDeskView { get; set; }


    private void Awake()
    {
        deskTrigger = GetComponentInChildren<DeskTrigger>();
        deskTrigger.InteractVisual = GetComponent<InteractionVisual>();
        deskCameraRotationController = GetComponent<CameraRotationController>();

        deskTrigger.DeskTriggered += () => SetDeskViewActive(true);

        /*
        cinemachineDeskCamera.gameObject.SetActive(false);
        */
        cinemachineDeskCamera.enabled = false;

        SetDeskCameraRotationEnabled(false);
        CanExitDeskView = true;
    }


    private void Start()
    {
        GameInput.OnExitDeskViewAction += () =>
        {
            if (CanExitDeskView)
            {
                SetDeskViewActive(false);
            }
        };
    }


    private void SetDeskViewActive(bool active)
    {
        IsDeskViewActive = active;

        // Disable or enable desk trigger.
        deskTrigger.gameObject.SetActive(!active);

        // Disable or enable player movement.
        PlayerScriptsController.SetPlayerMovementEnabled(!active);

        // Enable or disable desk camera control.
        SetDeskCameraRotationEnabled(active);

        // Invoke enter/exit event.
        DeskViewEnabled?.Invoke(active);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (active)
        {
            GameInput.PlayerInputActions.PlayerWalking.Disable();
            CameraController.SetActiveCinemachineCamera(cinemachineDeskCamera);
            // To reset camera rotation when entering desk view, uncomment the following line:
            // deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);
            GameInput.PlayerInputActions.Desk.Enable();
        }
        else
        {
            GameInput.PlayerInputActions.Desk.Disable();
            CameraController.SetActiveCinemachineCamera(CameraController.CinemachineMainCamera);
            GameInput.PlayerInputActions.PlayerWalking.Enable();
        }        
    }

    public void SetDeskCameraRotationEnabled(bool enabled)
    {
        IsDeskCameraRotationEnabled = enabled;
        deskCameraRotationController.enabled = enabled;
    }
}
