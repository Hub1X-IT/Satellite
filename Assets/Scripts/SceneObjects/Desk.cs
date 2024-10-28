using System;
using Unity.Cinemachine;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public event Action<bool> DeskViewEnterExit;

    [SerializeField]
    private CinemachineCamera cinemachineDeskCamera;

    private DeskTrigger deskTrigger;

    private CameraRotationController deskCameraRotationController;

    private readonly Vector3 deskCameraDefaultRotation = new(0f, 180f, 0f);

    private bool isDeskCameraRotationEnabled;

    private bool isInDeskView;


    public bool CanExitDeskView { get; set; }

    public bool IsDeskCameraRotationEnabled
    {
        get => isDeskCameraRotationEnabled;
        set
        {
            // Enable/disable desk camera control
            isDeskCameraRotationEnabled = value;
            deskCameraRotationController.enabled = value;            
        }
    }

    private bool IsInDeskView
    {
        get => isInDeskView;
        set
        {
            // Enter/exit desk view
            isInDeskView = value;
            EnterExitDeskView(value);
        }
    }


    private void Awake()
    {
        deskTrigger = GetComponentInChildren<DeskTrigger>();
        deskTrigger.InteractVisual = GetComponent<InteractionVisual>();
        deskCameraRotationController = GetComponent<CameraRotationController>();

        deskTrigger.DeskTriggered += () => IsInDeskView = true;

        cinemachineDeskCamera.gameObject.SetActive(false);

        IsDeskCameraRotationEnabled = false;
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


    private void EnterExitDeskView(bool state)
    {
        // Disable or enable desk trigger.
        deskTrigger.gameObject.SetActive(!state);

        // Disable or enable player movement.
        PlayerScriptsController.IsPlayerMovementEnabled = !state;

        // Enable or disable desk camera control.
        IsDeskCameraRotationEnabled = state;

        // Invoke enter/exit event.
        DeskViewEnterExit?.Invoke(state);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (state)
        {
            GameInput.PlayerInputActions.PlayerWalking.Disable();
            CameraController.ActiveCinemachineCamera = cinemachineDeskCamera;
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
    }
}
