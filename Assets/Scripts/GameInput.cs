using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput
{
    public static PlayerInputActions PlayerInputActions { get; private set; }

    public static Vector2 MovementVectorNormalized
    {
        get => PlayerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;
    }

    public static Vector2 RotationVector
    {
        get => PlayerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>();
    }

    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnSmartphoneToggleAction;

    public static event Action OnExitDeskViewAction;

    public static event Action OnLaptopAndMonitorExitAction;

    public static event Action OnMonitorExitAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnSubmitAction;


    public static void InitializeInput()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.All.Enable();
        PlayerInputActions.PlayerWalkingAndDesk.Enable();
        PlayerInputActions.PlayerWalking.Enable();
        PlayerInputActions.LaptopAndMonitor.Disable();
        PlayerInputActions.Monitor.Disable(); // should be enabled when testing monitor command prompt

        PlayerInputActions.All.Pause.performed += Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed += Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed += SmartphoneToggle_performed;

        PlayerInputActions.Desk.Interact.performed += Interact_performed; // Interact in Desk does the same thing as Interact in PlayerWalking
        PlayerInputActions.Desk.ExitDeskView.performed += ExitDeskView_performed;

        PlayerInputActions.LaptopAndMonitor.Exit.performed += LaptopAndMonitorExit_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        PlayerInputActions.Monitor.Exit.performed += MonitorExit_performed;
        PlayerInputActions.Monitor.Submit.performed += MonitorSubmit_performed;
    }


    public static void RemoveInput()
    {
        PlayerInputActions.All.Pause.performed -= Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed -= SmartphoneToggle_performed;

        PlayerInputActions.Desk.Interact.performed -= Interact_performed;
        PlayerInputActions.Desk.ExitDeskView.performed -= ExitDeskView_performed;

        PlayerInputActions.LaptopAndMonitor.Exit.performed -= LaptopAndMonitorExit_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        PlayerInputActions.Monitor.Exit.performed -= MonitorExit_performed;
        PlayerInputActions.Monitor.Submit.performed -= MonitorSubmit_performed;


        PlayerInputActions.Dispose();
    }

    private static void Keyboard_onTextInput(char c) { if (PlayerInputActions.Monitor.enabled) OnKeyboardInputAction?.Invoke(c); }

    private static void Pause_performed(InputAction.CallbackContext _) { OnPauseAction?.Invoke(); }

    private static void Interact_performed(InputAction.CallbackContext _) { OnInteractAction?.Invoke(); }

    private static void SmartphoneToggle_performed(InputAction.CallbackContext _) { OnSmartphoneToggleAction?.Invoke(); }

    private static void ExitDeskView_performed(InputAction.CallbackContext _) { OnExitDeskViewAction?.Invoke(); }

    private static void LaptopAndMonitorExit_performed(InputAction.CallbackContext _) { OnLaptopAndMonitorExitAction?.Invoke(); }

    private static void MonitorExit_performed(InputAction.CallbackContext _) { OnMonitorExitAction?.Invoke(); }

    private static void MonitorSubmit_performed(InputAction.CallbackContext _) { OnSubmitAction?.Invoke(); }
}