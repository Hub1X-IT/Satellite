using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInput
{
    public static PlayerInputActions PlayerInputActions { get; private set; }


    public static Vector2 MovementVectorNormalized => PlayerInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;

    public static Vector2 RotationVector => PlayerInputActions.PlayerWalking.Rotate.ReadValue<Vector2>();

    // public static Vector2 MouseDelta => PlayerInputActions.Computer.MouseDelta.ReadValue<Vector2>();

    public static float MouseScroll => PlayerInputActions.CommandPrompt.MouseScroll.ReadValue<Vector2>().y;


    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnSmartphoneToggleAction;
    public static event Action OnGuidebookToggleAction;

    public static event Action OnNextDialogueSentenceAction;

    public static event Action OnGuidebookChangePageLeftAction;
    public static event Action OnGuidebookChangePageRightAction;

    public static event Action OnComputerExitAction;
    public static event Action OnLeftClickPerformedAction;
    public static event Action OnReturnPerformedAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnCommandSubmitAction;


    public static void InitializeInput()
    {
        PlayerInputActions = new();

        PlayerInputActions.All.Enable();
        PlayerInputActions.PlayerWalking.Enable();
        PlayerInputActions.Dialogue.Disable();
        PlayerInputActions.Guidebook.Disable();
        PlayerInputActions.Computer.Disable();
        PlayerInputActions.CommandPrompt.Disable();

        PlayerInputActions.All.Pause.performed += Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed += Interact_performed;

        PlayerInputActions.PlayerWalking.SmartphoneToggle.performed += SmartphoneToggle_performed;
        PlayerInputActions.PlayerWalking.GuidebookToggle.performed += GuidebookToggle_performed;

        PlayerInputActions.Dialogue.NextSentence.performed += NextDialogueSentence_performed;

        PlayerInputActions.Guidebook.ChangePageRight.performed += GuidebookChangePageLeft_performed;
        PlayerInputActions.Guidebook.ChangePageLeft.performed += GuidebookChangePageRight_performed;

        PlayerInputActions.Computer.Exit.performed += ComputerExit_performed;
        PlayerInputActions.Computer.LeftClick.performed += LeftClick_performed;
        PlayerInputActions.Computer.Return.performed += Return_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed += CommandSubmit_performed;
    }

    public static void RemoveInput()
    {
        PlayerInputActions.All.Pause.performed -= Pause_performed;

        PlayerInputActions.PlayerWalking.Interact.performed -= Interact_performed;
        PlayerInputActions.PlayerWalking.SmartphoneToggle.performed -= SmartphoneToggle_performed;
        PlayerInputActions.PlayerWalking.GuidebookToggle.performed -= GuidebookToggle_performed;

        PlayerInputActions.Dialogue.NextSentence.performed -= NextDialogueSentence_performed;

        PlayerInputActions.Guidebook.ChangePageRight.performed -= GuidebookChangePageLeft_performed;
        PlayerInputActions.Guidebook.ChangePageLeft.performed -= GuidebookChangePageRight_performed;

        PlayerInputActions.Computer.Exit.performed -= ComputerExit_performed;
        PlayerInputActions.Computer.LeftClick.performed -= LeftClick_performed;
        PlayerInputActions.Computer.Return.performed -= Return_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        PlayerInputActions.CommandPrompt.CommandSubmit.performed -= CommandSubmit_performed;

        PlayerInputActions.Dispose();

        OnPauseAction = null;
        OnInteractAction = null;
        OnSmartphoneToggleAction = null;
        OnGuidebookToggleAction = null;
        OnNextDialogueSentenceAction = null;
        OnGuidebookChangePageLeftAction = null;
        OnGuidebookChangePageRightAction = null;
        OnComputerExitAction = null;
        OnLeftClickPerformedAction = null;
        OnKeyboardInputAction = null;
        OnReturnPerformedAction = null;
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

    private static void NextDialogueSentence_performed(InputAction.CallbackContext _) => OnNextDialogueSentenceAction?.Invoke();

    private static void GuidebookChangePageLeft_performed(InputAction.CallbackContext _) => OnGuidebookChangePageLeftAction?.Invoke();
    private static void GuidebookChangePageRight_performed(InputAction.CallbackContext _) => OnGuidebookChangePageRightAction?.Invoke();

    private static void ComputerExit_performed(InputAction.CallbackContext _) => OnComputerExitAction?.Invoke();
    private static void LeftClick_performed(InputAction.CallbackContext _) => OnLeftClickPerformedAction?.Invoke();
    private static void Return_performed(InputAction.CallbackContext _) => OnReturnPerformedAction?.Invoke();

    private static void CommandSubmit_performed(InputAction.CallbackContext _) => OnCommandSubmitAction?.Invoke();


    public static void SetMousePosition(Vector2 position)
    {
        Mouse.current.WarpCursorPosition(position);
    }
}