using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    

    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnPauseAction;

    public event EventHandler OnInteractAction;

    public event EventHandler OnExitAction;


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

        // !!!!!!!!!!!!!! disable !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        playerInputActions.Monitor.Enable();


        playerInputActions.PlayerWalkingAndDesk.Interact.performed += Interact_performed;
        playerInputActions.All.Pause.performed += Pause_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;
        playerInputActions.Monitor.Exit.performed += Exit_performed;
        playerInputActions.Monitor.Submit.performed += Submit_performed;
    }


    private void OnDestroy() {
        playerInputActions.PlayerWalkingAndDesk.Interact.performed -= Interact_performed;
        playerInputActions.All.Pause.performed -= Pause_performed;


        Keyboard.current.onTextInput -= Keyboard_onTextInput;
        playerInputActions.Monitor.Exit.performed -= Exit_performed;
        playerInputActions.Monitor.Submit.performed -= Submit_performed;


        playerInputActions.Dispose();
    }

    /*
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    */

    private void Interact_performed(InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
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
            OnKeyboardInputAction?.Invoke(this, new OnKeyboardInputActionEventArgs {
                key = c
            });
        }        
    }


    private void Pause_performed(InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }


    private void Exit_performed(InputAction.CallbackContext obj) {
        OnExitAction?.Invoke(this, EventArgs.Empty);
    }


    private void Submit_performed(InputAction.CallbackContext obj) {
        OnSubmitAction?.Invoke(this, EventArgs.Empty);
    }


    public PlayerInputActions GetInputActions() {
        return playerInputActions;
    }
}