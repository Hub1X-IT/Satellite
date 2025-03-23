using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInput
{
    public static PlayerInputActions PlayerInputActions { get; private set; }


    public static Vector2 MovementVectorNormalized => PlayerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;

    public static Vector2 RotationVector => PlayerInputActions.PlayerWalkingAndDesk.Rotate.ReadValue<Vector2>();

    // public static Vector2 CursorPosition => PlayerInputActions.All.CursorPosition.ReadValue<Vector2>();

    public static Vector2 MouseDelta => PlayerInputActions.Computer.MouseDelta.ReadValue<Vector2>();

    public static float MouseScroll => PlayerInputActions.CommandPrompt.MouseScroll.ReadValue<Vector2>().y;


    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnSmartphoneToggleAction;

    public static event Action OnGuidebookToggleAction;

    public static event Action OnComputerExitAction;

    public static event Action OnLeftClickPerformedAction;

    public static event Action OnChangeComputerLeftAction;

    public static event Action OnChangeComputerRightAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnCommandSubmitAction;


    public static void InitializeInput()
    {
        PlayerInputActions = new();

        PlayerInputActions.All.Enable();
        PlayerInputActions.PlayerWalkingAndDesk.Enable();
        PlayerInputActions.PlayerWalking.Enable();
        PlayerInputActions.Computer.Disable();
        PlayerInputActions.CommandPrompt.Disable();

        PlayerInputActions.All.Pause.performed += Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed += Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed += SmartphoneToggle_performed;
        PlayerInputActions.PlayerWalkingAndDesk.GuidebookToggle.performed += GuidebookToggle_performed;

        PlayerInputActions.Computer.Exit.performed += ComputerExit_performed;
        PlayerInputActions.Computer.LeftClick.performed += LeftClick_performed;
        PlayerInputActions.Computer.ChangeComputerLeft.performed += ChangeComputerLeft_performed;
        PlayerInputActions.Computer.ChangeComputerRight.performed += ChangeComputerRight_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed += CommandSubmit_performed;
    }

    public static void RemoveInput()
    {
        PlayerInputActions.All.Pause.performed -= Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        PlayerInputActions.PlayerWalkingAndDesk.SmartphoneToggle.performed -= SmartphoneToggle_performed;
        PlayerInputActions.PlayerWalkingAndDesk.GuidebookToggle.performed -= GuidebookToggle_performed;

        PlayerInputActions.Computer.Exit.performed -= ComputerExit_performed;
        PlayerInputActions.Computer.LeftClick.performed -= LeftClick_performed;
        PlayerInputActions.Computer.ChangeComputerLeft.performed -= ChangeComputerLeft_performed;
        PlayerInputActions.Computer.ChangeComputerRight.performed -= ChangeComputerRight_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed -= CommandSubmit_performed;

        PlayerInputActions.Dispose();

        OnPauseAction = null;
        OnInteractAction = null;
        OnSmartphoneToggleAction = null;
        OnGuidebookToggleAction = null;
        OnComputerExitAction = null;
        OnLeftClickPerformedAction = null;
        OnKeyboardInputAction = null;
        OnChangeComputerLeftAction = null;
        OnChangeComputerRightAction = null;
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

    private static void GuidebookToggle_performed(InputAction.CallbackContext _) => OnGuidebookToggleAction?.Invoke();

    private static void ComputerExit_performed(InputAction.CallbackContext _) => OnComputerExitAction?.Invoke();

    private static void LeftClick_performed(InputAction.CallbackContext _) => OnLeftClickPerformedAction?.Invoke();

    private static void ChangeComputerLeft_performed(InputAction.CallbackContext _) => OnChangeComputerLeftAction?.Invoke();

    private static void ChangeComputerRight_performed(InputAction.CallbackContext _) => OnChangeComputerRightAction?.Invoke();

    private static void CommandSubmit_performed(InputAction.CallbackContext _) => OnCommandSubmitAction?.Invoke();


    public static void SetMousePosition(Vector2 position)
    {
        Mouse.current.WarpCursorPosition(position);
    }
}