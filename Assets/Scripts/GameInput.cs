using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class GameInput : MonoBehaviour {
    

    public static GameInput Instance { get; private set; }
    

    public event EventHandler OnInteractAction;

    public event EventHandler OnExitAction;


    public event EventHandler<OnKeyboardInputActionEventArgs> OnKeyboardInputAction;
    public class OnKeyboardInputActionEventArgs : EventArgs {        
        public char key;
    }

    public event EventHandler OnRemoveAction;

    private PlayerInputActions playerInputActions;


    private void Awake() {
        Instance = this;


        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerWalking.Enable();


        // playerInputActions.Monitor.Enable();


        playerInputActions.PlayerWalking.Interact.performed += Interact_performed;
        //playerInputActions.PlayerWalking.Pause.performed += Pause_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;
        playerInputActions.Monitor.Exit.performed += Exit_performed;
        playerInputActions.Monitor.Remove.performed += Remove_performed;
    }


    private void OnDestroy() {
        playerInputActions.PlayerWalking.Interact.performed -= Interact_performed;
        //playerInputActions.PlayerWalking.Pause.performed -= Pause_performed;


        Keyboard.current.onTextInput -= Keyboard_onTextInput;
        playerInputActions.Monitor.Exit.performed -= Exit_performed;
        playerInputActions.Monitor.Remove.performed -= Remove_performed;


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
        Vector2 rotationVector = playerInputActions.PlayerWalking.Rotate.ReadValue<Vector2>();
        return rotationVector;
    }


    private void Keyboard_onTextInput(char obj) {
        if (playerInputActions.Monitor.enabled) {
            OnKeyboardInputAction?.Invoke(this, new OnKeyboardInputActionEventArgs {
                key = obj
            });
        }
    }


    private void Exit_performed(InputAction.CallbackContext obj) {
        OnExitAction?.Invoke(this, EventArgs.Empty);
    }


    private void Remove_performed(InputAction.CallbackContext obj) {
        OnRemoveAction?.Invoke(this, EventArgs.Empty);
    }


    public PlayerInputActions GetInputActions() {
        return playerInputActions;
    }
}