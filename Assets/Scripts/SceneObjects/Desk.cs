using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public event Action<bool> DeskViewEnabled;

    [SerializeField]
    private InteractionTrigger deskTrigger;

    [SerializeField]
    private CinemachineCamera deskCinemachineCamera;    

    private CameraRotationController deskCameraRotationController;

    /*
    private readonly Vector3 deskCameraDefaultRotation = new(0f, 180f, 0f);
    */

    public bool CanExitDeskView { get; set; }

    public CinemachineCamera DeskCinemachineCamera => deskCinemachineCamera;


    private void Awake()
    {
        deskTrigger.InteractVisual = GetComponent<InteractionVisual>();
        deskCameraRotationController = GetComponent<CameraRotationController>();

        GameInput.OnExitDeskViewAction += () =>
        {
            if (CanExitDeskView)
            {
                SetDeskViewActive(false);
            }
        };

        deskTrigger.InteractionTriggered += () => SetDeskViewActive(true);

        deskCinemachineCamera.enabled = false;

        SetDeskCameraRotationEnabled(false);
        CanExitDeskView = true;
    }

    private void OnDestroy()
    {
        DeskViewEnabled = null;
    }

    private void SetDeskViewActive(bool active)
    {
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
            CameraController.SetActiveCinemachineCamera(DeskCinemachineCamera);
            /*
            // Reset camera rotation when entering desk view.
            deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);
            */
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
        deskCameraRotationController.enabled = enabled;
    }
}
