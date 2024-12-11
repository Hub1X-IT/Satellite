using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInput
{
    public static PlayerInputActions PlayerInputActions { get; private set; }


    public static Vector2 MovementVectorNormalized => PlayerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;

    public static Vector2 RotationVector => PlayerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>();

    public static Vector2 CursorPosition => PlayerInputActions.All.CursorPosition.ReadValue<Vector2>();


    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnSmartphoneToggleAction;

    public static event Action OnExitDeskViewAction;

    public static event Action OnComputerExitAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnCommandSubmitAction;


    public static void InitializeInput()
    {
        PlayerInputActions = new();

        PlayerInputActions.All.Enable();
        PlayerInputActions.PlayerWalkingAndDesk.Enable();
        PlayerInputActions.PlayerWalking.Enable();
        PlayerInputActions.Computer.Disable();
        PlayerInputActions.CommandPrompt.Disable(); // should be enabled when testing monitor command prompt

        PlayerInputActions.All.Pause.performed += Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed += Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed += SmartphoneToggle_performed;

        PlayerInputActions.Desk.Interact.performed += Interact_performed; // Interact in Desk does the same thing as Interact in PlayerWalking
        PlayerInputActions.Desk.ExitDeskView.performed += ExitDeskView_performed;
        PlayerInputActions.Computer.Exit.performed += ComputerExit_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed += CommandSubmit_performed;
    }

    public static void RemoveInput()
    {
        PlayerInputActions.All.Pause.performed -= Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed -= SmartphoneToggle_performed;

        PlayerInputActions.Desk.Interact.performed -= Interact_performed;
        PlayerInputActions.Desk.ExitDeskView.performed -= ExitDeskView_performed;

        PlayerInputActions.Computer.Exit.performed -= ComputerExit_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed -= CommandSubmit_performed;

        PlayerInputActions.Dispose();

        OnPauseAction = null;
        OnInteractAction = null;
        OnSmartphoneToggleAction = null;
        OnExitDeskViewAction = null;
        OnComputerExitAction = null;
        OnKeyboardInputAction = null;
        OnCommandSubmitAction = null;
    }

    private static void Keyboard_onTextInput(char c)
    {
        if (PlayerInputActions.CommandPrompt.enabled)
        {
            OnKeyboardInputAction?.Invoke(c);
        }
    }

    private static void Pause_performed(InputAction.CallbackContext _) => OnPauseAction?.Invoke();

    private static void Interact_performed(InputAction.CallbackContext _) => OnInteractAction?.Invoke();

    private static void SmartphoneToggle_performed(InputAction.CallbackContext _) => OnSmartphoneToggleAction?.Invoke();

    private static void ExitDeskView_performed(InputAction.CallbackContext _) => OnExitDeskViewAction?.Invoke();

    private static void ComputerExit_performed(InputAction.CallbackContext _) => OnComputerExitAction?.Invoke();

    private static void CommandSubmit_performed(InputAction.CallbackContext _) => OnCommandSubmitAction?.Invoke();


    public static void SetMousePosition(Vector2 position)
    {
        Mouse.current.WarpCursorPosition(position);
    }
}