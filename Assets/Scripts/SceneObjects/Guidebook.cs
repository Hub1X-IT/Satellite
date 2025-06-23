using Unity.Cinemachine;
using UnityEngine;

public class Guidebook : MonoBehaviour
{
    private Desk desk;

    [SerializeField]
    private GameEventBoolSO guidebookViewToggledGameEvent;

    [SerializeField]
    private InteractionTrigger guidebookTrigger;

    [SerializeField]
    private CinemachineCamera guidebookCinemachineCamera;

    private Outline outline;

    private bool isInGuidebookView;

    private void Awake()
    {
        desk = GetComponentInParent<Desk>();
        outline = GetComponent<Outline>();

        guidebookTrigger.InteractVisual = GetComponent<InteractionVisual>();

        guidebookTrigger.InteractionTriggered += () => SetGuidebookViewActive(true);

        // change ?
        GameInput.OnComputerExitAction += () =>
        {
            if (isInGuidebookView)
            {
                SetGuidebookViewActive(false);
            }
        };

        guidebookCinemachineCamera.enabled = false;

        isInGuidebookView = false;

        guidebookTrigger.gameObject.SetActive(true);
    }

    private void SetGuidebookViewActive(bool active)
    {
        isInGuidebookView = active;
        GameManager.IsInScreenView = active;

        // Disable or enable player movement.
        PlayerScriptsController.SetPlayerMovementEnabled(!active);

        PlayerScriptsController.SetCanShowPlayerHUD(!active);

        guidebookTrigger.gameObject.SetActive(!active);

        // Probably a temporary solution
        outline.enabled = !active;

        GameManager.SetCursorShown(active);

        // Disable/enable specific input actions.
        // Change active Cinemachine camera.
        if (active)
        {
            GameInput.PlayerInputActions.PlayerWalking.Disable();
            GameInput.PlayerInputActions.Guidebook.Enable();
            GameInput.PlayerInputActions.Computer.Enable();
            CameraController.SetActiveCinemachineCamera(guidebookCinemachineCamera);
        }
        else
        {
            GameInput.PlayerInputActions.Computer.Disable();
            GameInput.PlayerInputActions.Guidebook.Disable();
            GameInput.PlayerInputActions.PlayerWalking.Enable();
            CameraController.SetActiveCinemachineCamera(CameraController.CinemachineMainCamera);
        }

        guidebookViewToggledGameEvent.RaiseEvent(active);

        desk.PlayDeskSitSound();
    }
}
