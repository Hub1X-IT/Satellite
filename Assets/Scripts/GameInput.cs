using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    

    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnPauseAction;

    public event EventHandler OnInteractAction;

    public event EventHandler OnSmartphoneToggleAction;

    public event EventHandler OnExitDeskViewAction;

    public event EventHandler OnLaptopAndMonitorExitAction;

    public event EventHandler OnMonitorExitAction;

    public event EventHandler<OnKeyboardInputActionEventArgs> OnKeyboardInputAction;
    public class OnKeyboardInputActionEventArgs : EventArgs {        
        public char key;
    }

    public event EventHandler OnSubmitAction;


    private PlayerInputActions playerInputActions;


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

        playerInputActions.LaptopAndMonitor.Exit.performed += LaptopAndMonitorExit_performed; // update the naming style/convention

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


    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.PlayerWalking.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }


    public Vector2 GetRotationVector() {
        Vector2 rotationVector = playerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>();
        return rotationVector;
    }


    private void Keyboard_onTextInput(char c) {
        if (playerInputActions.Monitor.enabled) {
            OnKeyboardInputAction?.Invoke(this, new OnKeyboardInputActionEventArgs { key = c });
        }        
    }


    private void Pause_performed(InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }


    private void Interact_performed(InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }


    private void SmartphoneToggle_performed(InputAction.CallbackContext obj) {
        OnSmartphoneToggleAction?.Invoke(this, EventArgs.Empty);
    }

    private void ExitDeskView_performed(InputAction.CallbackContext obj) {
        OnExitDeskViewAction?.Invoke(this, EventArgs.Empty);
    }

    private void LaptopAndMonitorExit_performed(InputAction.CallbackContext obj) {
        OnLaptopAndMonitorExitAction?.Invoke(this, EventArgs.Empty);
    }

    private void MonitorExit_performed(InputAction.CallbackContext obj) {
        OnMonitorExitAction?.Invoke(this, EventArgs.Empty);
    }


    private void MonitorSubmit_performed(InputAction.CallbackContext obj) {
        OnSubmitAction?.Invoke(this, EventArgs.Empty);
    }


    public PlayerInputActions GetInputActions() {
        return playerInputActions;
    }
}