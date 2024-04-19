using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {


    public static GameInput Instance { get; private set; }
    

    public event EventHandler OnInteractAction;

    private PlayerInputActions playerInputActions;


    private void Awake() {
        Instance = this;


        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerWalking.Enable();

        playerInputActions.PlayerWalking.Interact.performed += Interact_performed;
        //playerInputActions.PlayerWalking.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInputActions.PlayerWalking.Interact.performed -= Interact_performed;
        //playerInputActions.PlayerWalking.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    /*
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    */

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
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
}