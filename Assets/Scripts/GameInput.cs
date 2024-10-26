using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;


    public event Action OnPauseAction;

    public event Action OnInteractAction;

    public event Action OnSmartphoneToggleAction;

    public event Action OnExitDeskViewAction;

    public event Action OnLaptopAndMonitorExitAction;

    public event Action OnMonitorExitAction;

    public event Action<char> OnKeyboardInputAction;

    public event Action OnSubmitAction;


    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.All.Enable();
        playerInputActions.PlayerWalkingAndDesk.Enable();
        playerInputActions.PlayerWalking.Enable();
        playerInputActions.LaptopAndMonitor.Disable();
        playerInputActions.Monitor.Disable(); // should be enabled when testing monitor command prompt

        playerInputActions.All.Pause.performed += Pause_performed;

        playerInputActions.PlayerWalking.Interact.performed += Interact_performed;

        playerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed += SmartphoneToggle_performed;

        playerInputActions.Desk.Interact.performed += Interact_performed; // Interact in Desk does the same thing as Interact in PlayerWalking
        playerInputActions.Desk.ExitDeskView.performed += ExitDeskView_performed;

        playerInputActions.LaptopAndMonitor.Exit.performed += LaptopAndMonitorExit_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        playerInputActions.Monitor.Exit.performed += MonitorExit_performed;
        playerInputActions.Monitor.Submit.performed += MonitorSubmit_performed;
    }
    

    private void OnDestroy() {        
        playerInputActions.All.Pause.performed -= Pause_performed;

        playerInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        playerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed -= SmartphoneToggle_performed;

        playerInputActions.Desk.Interact.performed -= Interact_performed;
        playerInputActions.Desk.ExitDeskView.performed -= ExitDeskView_performed;

        playerInputActions.LaptopAndMonitor.Exit.performed -= LaptopAndMonitorExit_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        playerInputActions.Monitor.Exit.performed -= MonitorExit_performed;
        playerInputActions.Monitor.Submit.performed -= MonitorSubmit_performed;


        playerInputActions.Dispose();
    }


    public Vector2 GetMovementVectorNormalized() { return playerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized; }

    public Vector2 GetRotationVector() { return playerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>(); }

    private void Keyboard_onTextInput(char c) { if (playerInputActions.Monitor.enabled) OnKeyboardInputAction?.Invoke(c); }

    private void Pause_performed(InputAction.CallbackContext _) { OnPauseAction?.Invoke(); }

    private void Interact_performed(InputAction.CallbackContext _) { OnInteractAction?.Invoke(); }

    private void SmartphoneToggle_performed(InputAction.CallbackContext _) { OnSmartphoneToggleAction?.Invoke(); }

    private void ExitDeskView_performed(InputAction.CallbackContext _) { OnExitDeskViewAction?.Invoke(); }

    private void LaptopAndMonitorExit_performed(InputAction.CallbackContext _) { OnLaptopAndMonitorExitAction?.Invoke(); }

    private void MonitorExit_performed(InputAction.CallbackContext _) { OnMonitorExitAction?.Invoke(); }

    private void MonitorSubmit_performed(InputAction.CallbackContext _) { OnSubmitAction?.Invoke(); }


    public PlayerInputActions GetInputActions() { return playerInputActions; }
}