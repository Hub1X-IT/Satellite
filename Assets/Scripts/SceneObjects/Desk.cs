using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public event Action<bool> DeskViewEnterExit;


    [SerializeField]
    private CinemachineCamera cinemachineDeskCamera;


    public bool CanExitDeskView { get; set; }


    private DeskTrigger deskTrigger;


    private CameraRotationController deskCameraRotationController;
    private readonly Vector3 deskCameraDefaultRotation = new(0f, 180f, 0f);


    private bool isDeskCameraRotationEnabled;
    public bool IsDeskCameraRotationEnabled
    {
        get => isDeskCameraRotationEnabled;
        set
        {
            deskCameraRotationController.enabled = value;
            isDeskCameraRotationEnabled = value;
        }
    }

    private bool isInDeskView;
    private bool IsInDeskView
    {
        get => isInDeskView;
        set
        {
            // Enter/exit desk view

            // Disable or enable desk trigger.
            deskTrigger.gameObject.SetActive(!value);

            // Disable or enable player movement.
            PlayerScriptsController.IsPlayerMovementEnabled = !value;

            // Enable or disable desk camera control.
            IsDeskCameraRotationEnabled = value;

            // Invoke enter/exit event.
            DeskViewEnterExit?.Invoke(value);

            // Disable/enable specific input actions.
            // Change active Cinemachine camera.
            if (value)
            {
                GameInput.PlayerInputActions.PlayerWalking.Disable();
                CameraController.CurrentCinemachineCamera = cinemachineDeskCamera;
                // To reset camera rotation when entering desk view, uncomment the following line:
                // deskCameraRotationController.SetLocalRotation(deskCameraDefaultRotation.x, deskCameraDefaultRotation.y);
                GameInput.PlayerInputActions.Desk.Enable();
            }
            else
            {
                GameInput.PlayerInputActions.Desk.Disable();
                CameraController.ChangeToCinemachineMainCamera();
                GameInput.PlayerInputActions.PlayerWalking.Enable();
            }

            isInDeskView = value;
        }
    }


    private void Awake()
    {
        deskTrigger = GetComponentInChildren<DeskTrigger>();

        deskTrigger.OnDeskTrigger += () => IsInDeskView = true;

        deskTrigger.InteractVisual = GetComponent<InteractionVisual>();

        deskCameraRotationController = GetComponent<CameraRotationController>();
        deskCameraRotationController.enabled = false;        

        cinemachineDeskCamera.gameObject.SetActive(false);

        CanExitDeskView = true;
    }


    private void Start()
    {
        GameInput.OnExitDeskViewAction += () =>
        {
            if (CanExitDeskView)
            {
                IsInDeskView = false;
            }
        };
    }
}
