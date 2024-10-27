using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput {

    private static PlayerInputActions playerInputActions;


    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnSmartphoneToggleAction;

    public static event Action OnExitDeskViewAction;

    public static event Action OnLaptopAndMonitorExitAction;

    public static event Action OnMonitorExitAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnSubmitAction;


    public static void InitializeInput() {
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
    

    public static void RemoveInput() {        
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


    public static Vector2 GetMovementVectorNormalized() { return playerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized; }

    public static Vector2 GetRotationVector() { return playerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>(); }

    private static void Keyboard_onTextInput(char c) { if (playerInputActions.Monitor.enabled) OnKeyboardInputAction?.Invoke(c); }

    private static void Pause_performed(InputAction.CallbackContext _) { OnPauseAction?.Invoke(); }

    private static void Interact_performed(InputAction.CallbackContext _) { OnInteractAction?.Invoke(); }

    private static void SmartphoneToggle_performed(InputAction.CallbackContext _) { OnSmartphoneToggleAction?.Invoke(); }

    private static void ExitDeskView_performed(InputAction.CallbackContext _) { OnExitDeskViewAction?.Invoke(); }

    private static void LaptopAndMonitorExit_performed(InputAction.CallbackContext _) { OnLaptopAndMonitorExitAction?.Invoke(); }

    private static void MonitorExit_performed(InputAction.CallbackContext _) { OnMonitorExitAction?.Invoke(); }

    private static void MonitorSubmit_performed(InputAction.CallbackContext _) { OnSubmitAction?.Invoke(); }


    public static PlayerInputActions GetInputActions() { return playerInputActions; }
}